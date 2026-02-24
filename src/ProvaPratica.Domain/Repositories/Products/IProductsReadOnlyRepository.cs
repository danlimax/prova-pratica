using ProvaPratica.Domain.Entities;

namespace ProvaPratica.Domain.Repositories.Products
{
    public interface IProductsReadOnlyRepository
    {
        Task<List<Product>> GetAll();
    }
}
