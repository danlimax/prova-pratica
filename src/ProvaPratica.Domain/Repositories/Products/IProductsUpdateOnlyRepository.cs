using ProvaPratica.Domain.Entities;

namespace ProvaPratica.Domain.Repositories.Products
{
    public interface IProductsUpdateOnlyRepository
    {
        Task<Product?> GetById(int id);
        void Update(Product product);
    }
}
