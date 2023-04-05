using DomecChallange.Data.Context;
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
        private readonly DomecChallangeDbContext _context;
        #endregion
        public ProductService(DomecChallangeDbContext context)
        {
            _context = context;
        }
        #region Methods
        public async Task<StatusDto<Product>> CreateAsync(Product item)
        {
            if (item == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid Data" };
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data added successfully", ReturnId = item.UniqueId , ReturnModel = item };
        }
        public IQueryable<Product> GetAll(bool withAsNoTracking = true) => withAsNoTracking ? _context.Products.AsNoTracking() : _context.Products;

        public async Task<Product> GetAsync(Guid uniqueId) => await _context.Products.SingleOrDefaultAsync(p => p.UniqueId == uniqueId);

        public async Task<Product> GetAsync(int code) => await _context.Products.SingleOrDefaultAsync(p => p.Code == code);

        public async Task<StatusDto<Product>> UpdateAsync(Product item)
        {
            if (item == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid Data" };
            var model = await GetAsync(item.UniqueId);
            if (model == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid Data" };
            model = item;
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data added successfully", ReturnId = model.UniqueId };
        }
        
        #endregion
    }
}
