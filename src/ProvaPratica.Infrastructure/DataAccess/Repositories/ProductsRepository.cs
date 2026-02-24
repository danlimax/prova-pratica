using Microsoft.EntityFrameworkCore;
using ProvaPratica.Domain.Entities;
using ProvaPratica.Domain.Filters;
using ProvaPratica.Domain.Repositories.Products;

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
             var product = await _dbContext.Products.FindAsync(id);

            _dbContext.Products.Remove(product!);
        }

        public async Task<List<Product>> GetAll(ProductFilter filter)
        {
            var query = _dbContext.Products.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(p => p.Category.Contains(filter.Category));

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            return await query.ToListAsync();
        }

        async Task<Product?> IProductsUpdateOnlyRepository.GetById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
        }

        async Task<Product?> IProductsReadOnlyRepository.GetById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
        }



        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
