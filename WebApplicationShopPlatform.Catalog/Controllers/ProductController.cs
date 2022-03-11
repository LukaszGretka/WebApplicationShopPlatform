using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Catalog.Extenstions;
using WebApplicationShopPlatform.Catalog.Models;
using WebApplicationShopPlatform.Catalog.Services.Abstract;
using WebApplicationShopPlatform.Shared.Models;
using WebApplicationShopPlatform.Shared.ModelsDTO;

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
        public async Task<ActionResult<ProductDTO>> GetById(int Id)
        {
            Product product = await _productService.GetProductById(Id);

            if (product is null)
            {
                return NotFound(new { Message = "Product no found" });
            }

            List<Product> products = new List<Product>();

            products.Add(product);
            return products.WrapToDTO().FirstOrDefault();
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategory(int categoryId)
        {
            if (!Enum.IsDefined(typeof(CategoryDTO), categoryId))
            {
                return BadRequest(new { Message = "Invaid category number" });
            }

            IEnumerable<Product> products = await _productService.GetProductsByCategory(categoryId);

            if (products is null)
            {
                return NotFound(new { Message = $"Products with category = {categoryId} no found" });
            }

            return new JsonResult(products.ToList().WrapToDTO());
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetById([FromQuery] string name)
        {
            IEnumerable<Product> products = await _productService.GetProductsByName(name);

            if (products is null)
            {
                return NotFound(new { Message = $"Products with name = {name} no found" });
            }

            return new JsonResult(products.ToList().WrapToDTO());
        }

        // TODO: only for manager role (need implementation of indentity)
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            DatabaseActionResult<Product> result = await _productService.Create(product);

            if (result.Exception != null)
            {
                return BadRequest("Can't add product to database. Please check used values.");
            }

            return CreatedAtAction(nameof(Create), new { id = result.Obj.Id }, result.Obj);
        }

        // TODO: only for manager role (need implementation of indentity)
        [HttpPut]
        [Route("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Update(int id, ProductDTO product)
        {
            if (id != product.Id)
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
        [Route("{id}")]
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
