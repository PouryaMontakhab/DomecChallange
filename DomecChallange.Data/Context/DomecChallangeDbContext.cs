using DomecChallange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Data.Context
{
    public class DomecChallangeDbContext: DbContext
    {
        #region Dbsets
        public DbSet<Product> Products => Set<Product>();
        #endregion
        public DomecChallangeDbContext(DbContextOptions<DomecChallangeDbContext> options) : base(options)
        {
        }
    }
}
