using AutoMapper;
using AutoMapper.QueryableExtensions;
using DomecChallange.Data.Context;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Enums;
using DomecChallange.Dtos.ProdcutDtos;
using DomecChallange.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DomecChallange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingController : ControllerBase
    {
        #region Props
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor
        public ShoppingController(
            IMapper mapper,
            IProductService productService)
        {
            _productService = productService;
            _mapper = mapper;
        }
        #endregion
        #region Methods
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() =>
            Ok(await _productService.GetAll().ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync());

        [HttpGet("{name}", Name = nameof(GetProductByName))]
        public async Task<ActionResult<ProductDto>> GetProductByName(string name)
        {
            var product = await _productService.GetAsync(name);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductDto item)
        {
            var validationMessage = _productService.CheckingEntryData(_mapper.Map<EditProductDto>(item));
            if (!validationMessage.IsValid)
            {
                string alerts = string.Join(",", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));
                alerts += validationMessage.Message;
                ModelState.AddModelError("", alerts);
                throw new Exception(alerts);
            }
            var result = await _productService.CreateAsync(_mapper.Map<Product>(item));
            if (result.Status != StatusEnum.Success)
                throw new Exception(result.Message);
            return CreatedAtRoute(nameof(GetProductByName), new { name = result.ReturnModel.Name }, _mapper.Map<ProductDto>(result.ReturnModel));
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
            var result = await _productService.UpdateAsync(_mapper.Map<Product>(item));
            if (result.Status != StatusEnum.Success)
                throw new Exception(result.Message);
            return CreatedAtRoute(nameof(GetProductByName), new { name = result.ReturnModel.Name }, _mapper.Map<ProductDto>(result.ReturnModel));
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
