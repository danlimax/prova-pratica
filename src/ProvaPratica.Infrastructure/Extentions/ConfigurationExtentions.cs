using Microsoft.Extensions.Configuration;

namespace ProvaPratica.Infrastructure.Extentions
{
    public static class ConfigurationExtensions
    {
        public static bool IsTestEnvironment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
    }
}
