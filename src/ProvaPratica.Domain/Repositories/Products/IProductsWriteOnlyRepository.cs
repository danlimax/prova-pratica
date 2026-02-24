using ProvaPratica.Domain.Entities;

namespace ProvaPratica.Domain.Repositories.Products
{
    public interface IProductsWriteOnlyRepository
    {
        Task Add(Product product);

        Task Delete(int id);
    }
}
