using ProvaPratica.Communication.Requests;

namespace ProvaPratica.Application.Products.Update
{
    public interface IUpdateProductUseCase
    {
        Task Execute(int id, RequestProductJson request);
    }
}
