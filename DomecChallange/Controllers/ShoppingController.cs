using DomecChallange.Data.Context;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Enums;
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
        #region Props
        private readonly IProductService _productService;
        #endregion
        #region Ctor
        public ShoppingController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion
        #region Methods
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
        [HttpGet("{id}", Name = nameof(GetProduct))]
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
            if (result.Status != StatusEnum.Success)
                throw new Exception(result.Message);
            var returnModel = new ProductDto
            {
                UniqueId = result.ReturnModel.UniqueId,
                Code = result.ReturnModel.Code,
                Name = result.ReturnModel.Name,
                Quantity = result.ReturnModel.Quantity,
            };
            return CreatedAtRoute(nameof(GetProduct), new { id = result.ReturnId }, returnModel);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditProductDto item)
        {
            var result = await _productService.UpdateAsync(new Product
            {
                UniqueId = item.UniqueId,
                Name = item.Name,
                Quantity = item.Quantity,
            });
            if (result.Status != StatusEnum.Success)
                throw new Exception(result.Message);
            var returnModel = new ProductDto
            {
                UniqueId = result.ReturnModel.UniqueId,
                Code = result.ReturnModel.Code,
                Name = result.ReturnModel.Name,
                Quantity = result.ReturnModel.Quantity,
            };
            return CreatedAtRoute(nameof(GetProduct), new { id = result.ReturnId }, returnModel);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid uniqueId)
        {
            var result = await _productService.DeleteAsync(uniqueId);
            if (result.Status != StatusEnum.Success)
                throw new Exception(result.Message);
            return Ok();
        }
        #endregion
    }
}
