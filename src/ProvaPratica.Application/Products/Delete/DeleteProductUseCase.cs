using ProvaPratica.Domain.Repositories;
using ProvaPratica.Domain.Repositories.Products;
using ProvaPratica.Exception;
using ProvaPratica.Exception.ExceptionsBase;

namespace ProvaPratica.Application.Products.Delete
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductsReadOnlyRepository _productReadOnly;
        private readonly IProductsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductUseCase(IUnitOfWork unitOfWork,
        IProductsWriteOnlyRepository repository,
        IProductsReadOnlyRepository productReadOnly)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _productReadOnly = productReadOnly;
        }

        public async Task Execute(int id)
        {

            var product = await _productReadOnly.GetById(id);

            if (product is null)
            {
                throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
            }

            await _repository.Delete(id);

            await _unitOfWork.Commit();
        }
    }
}
