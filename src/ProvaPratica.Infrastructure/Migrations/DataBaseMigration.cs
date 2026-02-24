using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProvaPratica.Infrastructure.DataAccess;

namespace ProvaPratica.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public static async Task MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ProvaPraticaDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
