using AutoMapper;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;
using ProvaPratica.Domain.Entities;
using ProvaPratica.Domain.Filters;
using ProvaPratica.Domain.Repositories.Products;

namespace ProvaPratica.Application.Products.GetAll
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductsReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsUseCase(IProductsReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseProductsJson> Execute(ProductsFilter filters)
        {

            var domainFilter = _mapper.Map<ProductFilter>(filters);

            var result = await _repository.GetAll(domainFilter);

            return new ResponseProductsJson
            {
                Products = _mapper.Map<List<ResponseProductJson>>(result)
            };

        }
    }
}
