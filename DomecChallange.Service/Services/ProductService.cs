using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Dtos.Enums;
using DomecChallange.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Service.Services
{
    public class ProductService : IProductService
    {
        #region Props
        private readonly DbSet<Product> _product;
        #endregion
        #region Methods
        public async Task<StatusDto> CreateAsync(Product item)
        {
            if (item == null) return new StatusDto { Status = StatusEnum.Error, Message = "Invalid Data" };
            await _product.AddAsync(item);
            return new StatusDto { Status = StatusEnum.Success, Message = "Data added successfully" };
        }
        public IQueryable<Product> GetAll(bool withAsNoTracking = true) => withAsNoTracking ? _product.AsNoTracking() : _product;

        public async Task<Product> GetAsync(Guid uniqueId) => await _product.SingleOrDefaultAsync(p => p.UniqueId == uniqueId);

        public async Task<Product> GetAsync(int code) => await _product.SingleOrDefaultAsync(p => p.Code == code);

        public async Task<StatusDto> UpdateAsync(Product item)
        {
            if (item == null) return new StatusDto { Status = StatusEnum.Error, Message = "Invalid Data" };
            var model = await GetAsync(item.UniqueId);
            if (model == null) return new StatusDto { Status = StatusEnum.Error, Message = "Invalid Data" };
            model = item;
            return new StatusDto { Status = StatusEnum.Success, Message = "Data added successfully", ReturnId = model.UniqueId };
        }
        #endregion
    }
}
