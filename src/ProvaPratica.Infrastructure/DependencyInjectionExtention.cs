using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProvaPratica.Domain.Repositories;
using ProvaPratica.Domain.Repositories.Products;
using ProvaPratica.Infrastructure.DataAccess;
using ProvaPratica.Infrastructure.DataAccess.Repositories;
using ProvaPratica.Infrastructure.Extentions;

namespace ProvaPratica.Infrastructure
{
    public static class DependencyInjectionExtention
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            
            if (configuration.IsTestEnvironment() == false)
            {
                AddDbContext(services, configuration);
            }
        }

        private static void AddRepositories(IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();



            services.AddScoped<IProductsWriteOnlyRepository, ProductsRepository>();
            services.AddScoped<IProductsReadOnlyRepository, ProductsRepository>();
            services.AddScoped<IProductsUpdateOnlyRepository, ProductsRepository>();



        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ProvaPraticaDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

       
    }
}
