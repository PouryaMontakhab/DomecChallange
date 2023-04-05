using DomecChallange.Data.Codes;
using DomecChallange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomecChallange.Data.Context
{
    public class DomecChallangeDbContext : DbContext
    {
        #region Dbsets
        public DbSet<Product> Products => Set<Product>();
        #endregion
        public DomecChallangeDbContext(DbContextOptions<DomecChallangeDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(code =>
            {
                code.Property(p => p.Code).HasValueGenerator<CodeGenerator>();
            });
        }
    }
}
