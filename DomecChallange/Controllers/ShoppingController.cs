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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            return await _productService.GetAll().Select(a => new ProductDto
            {
                UniqueId = a.UniqueId,
                Code = a.Code,
                Name = a.Name,
                Quantity = a.Quantity,
            }).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(Guid uniqueId)
        {
            var product = await _productService.GetAsync(uniqueId);
            if (product == null) return NotFound();
            return new ProductDto
            {
                UniqueId = product.UniqueId,
                Code = product.Code,
                Name = product.Name,
                Quantity = product.Quantity,
            };
        }


        [HttpPost]
        public async Task<ActionResult<AddProductDto>> Post([FromBody]AddProductDto item)
        {
            var result = await _productService.CreateAsync(new Product {
                Name = item.Name,
                Quantity = item.Quantity,
            });
            return CreatedAtAction(nameof(Get), new { uniqueId = result.ReturnId });
        }
    }
}
