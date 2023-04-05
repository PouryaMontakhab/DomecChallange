using DomecChallange.Domain.Entities;
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
        IQueryable<Product> GetAll();
        Task<Product> GetAsync(Guid uniqueId);
        Task<Product> GetAsync(int code);
        void CreateAsync(Product item);
        void UpdateAsync(Product item);
    }
}
