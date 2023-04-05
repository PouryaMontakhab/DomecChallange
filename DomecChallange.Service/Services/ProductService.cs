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

        public async Task<bool> CheckExistAsync(string name, Guid? id = null)
        {
            return await GetAll()
                    .AnyAsync(x =>
                             (string.IsNullOrEmpty(name) || x.Name.ToLower().Trim() == name.ToLower().Trim()) &&
                             (!id.HasValue || x.UniqueId != id));
        }
        #region Methods
        public async Task<StatusDto<Product>> CreateAsync(Product item)
        {
            if (item == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            if (await CheckExistAsync(item.Name)) return new StatusDto<Product> { Status = StatusEnum.Exists , Message="Data exists" };
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data added successfully", ReturnId = item.UniqueId , ReturnModel = item };
        }

        public async Task<StatusDto<Product>> DeleteAsync(Guid uniqueId)
        {
            if (uniqueId == new Guid()) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            var model = await GetAsync(uniqueId);
            if (model == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            _context.Products.Remove(model);
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data removed successfully"};
        }

        public IQueryable<Product> GetAll(bool withAsNoTracking = true) => withAsNoTracking ? _context.Products.AsNoTracking() : _context.Products;

        public async Task<Product> GetAsync(Guid uniqueId) => await _context.Products.SingleOrDefaultAsync(p => p.UniqueId == uniqueId);

        public async Task<Product> GetAsync(int code) => await _context.Products.SingleOrDefaultAsync(p => p.Code == code);

        public async Task<StatusDto<Product>> UpdateAsync(Product item)
        {
            if (item == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            var model = await GetAsync(item.UniqueId);
            if (model == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            if (await CheckExistAsync(item.Name,item.UniqueId)) return new StatusDto<Product> { Status = StatusEnum.Exists, Message = "Data exists" };
            model.UniqueId = item.UniqueId;
            model.Quantity = item.Quantity;
            model.Name = item.Name;
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data updated successfully", ReturnId = model.UniqueId, ReturnModel = model };
        }
        
        #endregion
    }
}
