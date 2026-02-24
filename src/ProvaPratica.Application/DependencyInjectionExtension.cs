using Microsoft.Extensions.DependencyInjection;
using ProvaPratica.Application.AutoMapper;
using ProvaPratica.Application.Products.Register;

namespace ProvaPratica.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {

            services.AddAutoMapper(cfg => cfg.AddProfile(typeof(AutoMapping)));
        }

        private static void AddUseCases(IServiceCollection services)
        {
            
            services.AddScoped<IRegisterProductsUseCase, RegisterProductUseCase>();
            

          
        }
    }
}
