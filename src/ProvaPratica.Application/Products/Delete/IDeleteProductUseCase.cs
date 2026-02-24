namespace ProvaPratica.Application.Products.Delete
{
    public interface IDeleteProductUseCase
    {
        Task Execute(int id);
    }
}
