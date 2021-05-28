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
        public async Task<ActionResult<Product>> GetById(int Id)
        {
            Product product = await _productService.GetProductById(Id);

            if (product is null)
            {
                return NotFound("Product no found");
            }

            return product;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetById([FromQuery] string name)
        {
            IEnumerable<Product> products = await _productService.GetProductByName(name);

            if (products is null)
            {
                return NotFound("Products no found");
            }

            return new JsonResult(products);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            DatabaseActionResult<Product> result = await _productService.AddProduct(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(AddProduct), new { id = result.Obj.ID }, result.Obj);
        }
    }
}
