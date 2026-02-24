using AutoMapper;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;
using ProvaPratica.Domain.Entities;
using ProvaPratica.Domain.Filters;

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
            CreateMap<ProductsFilter, ProductFilter>();
        }

        private void EntityToResponse()
        {
            CreateMap<Product,ResponseRegistredProductJson>();  
        }
    }
}
