using AutoMapper;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;
using ProvaPratica.Domain.Entities;


namespace ProvaPratica.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            RequestToEntity();
            EntityToResponse();
        }


        private void RequestToEntity()
        {
            CreateMap<RequestProductJson, Product>();
            CreateMap<ProductsFilter, Product>();
        }

        private void EntityToResponse()
        {
            CreateMap<Product,ResponseRegistredProductJson>();
            CreateMap<Product, ResponseProductJson>();
        }
    }
}
