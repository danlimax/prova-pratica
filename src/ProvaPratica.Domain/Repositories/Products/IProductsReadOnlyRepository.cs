using ProvaPratica.Domain.Entities;
using ProvaPratica.Domain.Filters;

namespace ProvaPratica.Domain.Repositories.Products
{
    public interface IProductsReadOnlyRepository
    {
        Task<List<Product>> GetAll(ProductFilter filters);
        Task<Product?> GetById(int id);
    }
}
