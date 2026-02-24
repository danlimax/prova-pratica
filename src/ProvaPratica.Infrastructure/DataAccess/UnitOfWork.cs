using ProvaPratica.Domain.Repositories;

namespace ProvaPratica.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProvaPraticaDbContext _dbContext;

        public UnitOfWork(ProvaPraticaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
