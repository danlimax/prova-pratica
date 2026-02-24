using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;

namespace ProvaPratica.Application.Products.Register
{
    public interface IRegisterProductsUseCase
    {
        Task<ResponseRegistredProductJson> Execute(RequestProductJson request);
    }
}
