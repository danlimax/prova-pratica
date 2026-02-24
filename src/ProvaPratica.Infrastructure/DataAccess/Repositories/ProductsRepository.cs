using Microsoft.EntityFrameworkCore;
using ProvaPratica.Domain.Entities;
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

        public async Task<List<Product>> GetAll
            (
            string? Category,
            decimal? MinPrice,
            decimal? MaxPrice,
            bool? Status,
            string? Image
            )
        {
            var query = _dbContext.Products.AsNoTracking().AsQueryable();

            if (Category is not null)
            {
                query = query.Where(product => product.Category.Contains(Category));
            }

            if (MinPrice is not null)
            {
                query = query.Where(product => product.Price >= MinPrice.Value);
            }

            if (MaxPrice is not null)
            {
                query = query.Where(product => product.Price <= MaxPrice.Value);
            }

            if (Status is not null)
            {
                query = query.Where(product => product.Status == Status.Value);
            }

            if (Image is not null)
            {
                query = query.Where(product => product.Image.Contains(Image));
            }

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
