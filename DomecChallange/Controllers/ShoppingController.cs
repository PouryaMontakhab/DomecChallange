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
        [HttpGet("{name}", Name = nameof(GetProductByName))]
        public async Task<ActionResult<ProductDto>> GetProductByName(string name)
        {
            var product = await _productService.GetAsync(name);
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
            var validationMessage = _productService.CheckingEntryData(new EditProductDto
            {
                Name = item.Name,
                Quantity = item.Quantity,
            });
            if (!validationMessage.IsValid)
            {
                string alerts = string.Join(",", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));
                alerts += validationMessage.Message;
                ModelState.AddModelError("", alerts);
                throw new Exception(alerts);
            }
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
            return CreatedAtRoute(nameof(GetProductByName), new { name = result.ReturnModel.Name }, returnModel);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditProductDto item)
        {
            var validationMessage = _productService.CheckingEntryData(item);
            if (!validationMessage.IsValid)
            {
                string alerts = string.Join(",", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));
                alerts += validationMessage.Message;
                ModelState.AddModelError("", alerts);
                throw new Exception(alerts);
            }
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
            return CreatedAtRoute(nameof(GetProductByName), new { name = result.ReturnModel.Name }, returnModel);
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
