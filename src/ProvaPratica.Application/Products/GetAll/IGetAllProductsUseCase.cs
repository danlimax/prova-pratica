using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;

namespace ProvaPratica.Application.Products.GetAll
{
    public interface IGetAllProductsUseCase
    {

        Task<ResponseProductsJson> Execute(ProductsFilter filter);
    }
}
