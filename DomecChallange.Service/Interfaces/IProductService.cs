using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Dtos.ProdcutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Service.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> GetAll(bool withAsNoTracking = true);
        Task<Product> GetAsync(Guid uniqueId);
        Task<Product> GetAsync(int code);
        Task<StatusDto<Product>> CreateAsync(Product item);
        Task<StatusDto<Product>> UpdateAsync(Product item);
        Task<StatusDto<Product>> DeleteAsync(Guid uniqueId);
        Task<bool> CheckExistAsync(string name, Guid? id = null);

    }
}
