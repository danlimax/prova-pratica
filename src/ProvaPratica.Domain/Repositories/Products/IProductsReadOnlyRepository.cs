using ProvaPratica.Domain.Entities;
 

namespace ProvaPratica.Domain.Repositories.Products
{
    public interface IProductsReadOnlyRepository
    {
        Task<List<Product>> GetAll
            (
            string? Category, 
            decimal? MinPrice, 
            decimal? MaxPrice, 
            bool? Status, 
            string? Image
            );
        Task<Product?> GetById(int id);
    }
}
