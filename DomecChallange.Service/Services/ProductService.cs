using DomecChallange.Domain.Entities;
using DomecChallange.Dtos.Codes;
using DomecChallange.Service.Interfaces;
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

        #endregion
        #region Ctor
        public ProductService()
        {

        }
        #endregion
        #region Methods
        public async Task<StatusDto> CreateAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetAsync(Guid uniqueId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetAsync(int code)
        {
            throw new NotImplementedException();
        }

        public async Task<StatusDto> UpdateAsync(Product item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
