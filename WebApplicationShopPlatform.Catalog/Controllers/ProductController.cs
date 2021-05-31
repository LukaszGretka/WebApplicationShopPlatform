using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.DTO;
using WebApplicationShopPlatform.Catalog.Models;
using WebApplicationShopPlatform.Catalog.Services.Abstract;

namespace WebApplicationShopPlatform.Catalog.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<Product>> GetById(int Id)
        {
            Product product = await _productService.GetProductById(Id);

            if (product is null)
            {
                return NotFound(new { Message = "Product no found" });
            }

            return product;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<Product>>> GetById([FromQuery] string name)
        {
            IEnumerable<Product> products = await _productService.GetProductByName(name);

            if (products is null)
            {
                return NotFound(new { Message = $"Products with name {name} no found" });
            }

            return new JsonResult(products);
        }

        // TODO: only for manager role (need implementation of indentity)
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Create(Product product)
        {
            DatabaseActionResult<Product> result = await _productService.Create(product);

            if (result.Exception != null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(Create), new { id = result.Obj.ID }, result.Obj);
        }

        // TODO: only for manager role (need implementation of indentity)
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            DatabaseActionResult<Product> result = await _productService.Update(id, product);

            if (result.Exception != null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (!result.Success)
            {
                return NotFound(new { Message = "Product no found" });
            }

            return NoContent();
        }

        // TODO: only for manager role or Ordering microservice (need implementation of indentity)
        [HttpDelete]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            DatabaseActionResult<Product> result = await _productService.DeleteById(id);

            if (result.Exception != null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (!result.Success)
            {
                return NotFound(new { Message = "Product no found" });
            }

            return NoContent();
        }
    }
}
