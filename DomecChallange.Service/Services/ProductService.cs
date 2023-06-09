﻿using DomecChallange.Data.Context;
using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Dtos.Enums;
using DomecChallange.Dtos.ProdcutDtos;
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
        #region Ctor
        public ProductService(DomecChallangeDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public async Task<StatusDto<Product>> CreateAsync(Product item)
        {
            if (item == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            if (await CheckExistAsync(item.Name)) return new StatusDto<Product> { Status = StatusEnum.Exists, Message = "Data exists" };
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data added successfully", ReturnId = item.UniqueId, ReturnModel = item };
        }
        public async Task<StatusDto<Product>> UpdateAsync(Product item)
        {
            if (item == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            var model = await GetAsync(item.UniqueId);
            if (model == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            if (await CheckExistAsync(item.Name, item.UniqueId)) return new StatusDto<Product> { Status = StatusEnum.Exists, Message = "Data exists" };
            model.UniqueId = item.UniqueId;
            model.Quantity = item.Quantity;
            model.Name = item.Name;
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data updated successfully", ReturnId = model.UniqueId, ReturnModel = model };
        }
        public async Task<StatusDto<Product>> DeleteAsync(Guid uniqueId)
        {
            if (uniqueId == new Guid()) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            var model = await GetAsync(uniqueId);
            if (model == null) return new StatusDto<Product> { Status = StatusEnum.Error, Message = "Invalid data" };
            _context.Products.Remove(model);
            await _context.SaveChangesAsync();
            return new StatusDto<Product> { Status = StatusEnum.Success, Message = "Data removed successfully" };
        }
        public IQueryable<Product> GetAll(PaginationFilterDto filter, bool withAsNoTracking = true)
            => withAsNoTracking ?
            _context.Products.AsNoTracking().Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
            : _context.Products.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
        public async Task<Product> GetAsync(Guid uniqueId) => await _context.Products.SingleOrDefaultAsync(p => p.UniqueId == uniqueId);
        public async Task<Product> GetAsync(int code) => await _context.Products.SingleOrDefaultAsync(p => p.Code == code);
        public async Task<Product> GetAsync(string name) => await _context.Products.SingleOrDefaultAsync(p => p.Name == name);
        public async Task<bool> CheckExistAsync(string name, Guid? id = null)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(x =>
                             (string.IsNullOrEmpty(name) || x.Name.ToLower().Trim() == name.ToLower().Trim()) &&
                             (!id.HasValue || x.UniqueId != id));
        }
        private bool IsValidQuantity(int quantity) => quantity >= 1;

        public ValidationMessageDto CheckingEntryData(EditProductDto item)
        {
            var errorMessage = new ValidationMessageDto();
            if (!IsValidQuantity(item.Quantity))
            {
                errorMessage.IsValid = false;
                errorMessage.AppendString("Quantity must be greather than zero");
            }
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                errorMessage.IsValid = false;
                errorMessage.AppendString("Product name must not be empty");
            }
            return errorMessage;
        }
        #endregion
    }
}
