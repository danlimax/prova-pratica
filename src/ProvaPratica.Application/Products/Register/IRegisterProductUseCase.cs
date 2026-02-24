using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;

namespace ProvaPratica.Application.Products.Register
{
    public interface IRegisterProductUseCase
    {
        Task<ResponseRegistredProductJson> Execute(RequestProductJson request);
    }
}
