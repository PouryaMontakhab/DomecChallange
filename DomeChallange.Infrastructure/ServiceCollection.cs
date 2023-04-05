using DomecChallange.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeChallange.Infrastructure
{
    public static class ServiceCollection
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DomecChallangeDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "DomecChallangeSystemContext"));
            services.AddScoped<DomecChallangeDbContext>();
        }
    }
}
