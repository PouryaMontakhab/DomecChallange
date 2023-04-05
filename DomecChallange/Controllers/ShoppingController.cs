using DomecChallange.Data.Context;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.ProdcutDtos;
using DomecChallange.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DomecChallange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IProductService _productService;

        public ShoppingController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAll().Select(a => new ProductDto
            {
                UniqueId = a.UniqueId,
                Code = a.Code,
                Name = a.Name,
                Quantity = a.Quantity,
            }).ToListAsync());
        }
        [HttpGet("{id}",Name =nameof(GetProduct))]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid uniqueId)
        {
            var product = await _productService.GetAsync(uniqueId);
            if (product == null) return NotFound();
            return Ok(new ProductDto
            {
                UniqueId = product.UniqueId,
                Code = product.Code,
                Name = product.Name,
                Quantity = product.Quantity,
            });
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductDto item)
        {
            var result = await _productService.CreateAsync(new Product
            {
                Name = item.Name,
                Quantity = item.Quantity,
            });
            var returnModel = new ProductDto
            {
                UniqueId = result.ReturnModel.UniqueId,
                Code = result.ReturnModel.Code,
                Name = result.ReturnModel.Name,
                Quantity = result.ReturnModel.Quantity,
            };
            return CreatedAtRoute(nameof(GetProduct), new {id = result.ReturnId },returnModel);

        }
    }
}
