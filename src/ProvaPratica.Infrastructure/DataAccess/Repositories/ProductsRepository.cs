using ProvaPratica.Domain.Entities;
using ProvaPratica.Domain.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace ProvaPratica.Infrastructure.DataAccess.Repositories
{
    public class ProductsRepository : IProductsReadOnlyRepository, IProductsUpdateOnlyRepository, IProductsWriteOnlyRepository
    {
        private readonly ProvaPraticaDbContext _dbContext;

        public ProductsRepository(ProvaPraticaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task Delete(int id)
        {
            var result = await _dbContext.Products.FindAsync(id);

            _dbContext.Products.Update(result!);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        async Task<Product?> IProductsUpdateOnlyRepository.GetById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(reward => reward.Id == id);
        }


        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
