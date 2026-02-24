using AutoMapper;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Domain.Repositories;
using ProvaPratica.Domain.Repositories.Products;
using ProvaPratica.Exception;
using ProvaPratica.Exception.ExceptionsBase;

namespace ProvaPratica.Application.Products.Update
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductsUpdateOnlyRepository _repository;

         public UpdateProductUseCase(IMapper mapper,
        IUnitOfWork unitOfWork,
        IProductsUpdateOnlyRepository repository)      
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
          
        }
        public async Task Execute(int id, RequestProductJson request)
        {
            Validate(request);
            var product = await _repository.GetById(id);

            if (product is null)
            {
                throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
            }

            _mapper.Map(request, product);

            _repository.Update(product);

            await _unitOfWork.Commit();
            
        }

        private void Validate(RequestProductJson request)
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
