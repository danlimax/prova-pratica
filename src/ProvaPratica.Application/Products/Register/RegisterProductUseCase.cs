using AutoMapper;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Communication.Responses;
using ProvaPratica.Domain.Entities;
using ProvaPratica.Domain.Repositories;
using ProvaPratica.Domain.Repositories.Products;
using ProvaPratica.Exception.ExceptionsBase;

namespace ProvaPratica.Application.Products.Register
{
    public class RegisterProductUseCase : IRegisterProductUseCase
    {
        private readonly IProductsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterProductUseCase(IProductsWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseRegistredProductJson> Execute(RequestProductJson request)
        {
            Validate(request);

            var product = _mapper.Map<Product>(request);

            await _repository.Add(product);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegistredProductJson>(product);
        }

        public void Validate(RequestProductJson request)
        {
            var validator = new ProductValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
