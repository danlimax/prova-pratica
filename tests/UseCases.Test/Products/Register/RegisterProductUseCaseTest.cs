using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using ProvaPratica.Application.Products.Register;
using ProvaPratica.Exception;
using Shouldly;

namespace UseCases.Test.Products.Register
{
    public class RegisterProductUseCaseTest
    {
        [Fact]
        public async Task Sucess(){
            var request = RequestProductJsonBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);


            result.ShouldNotBeNull();
            result.ProductName.ShouldBe(request.ProductName);
        }

        [Fact]
        public async Task Fail_When_ProductName_Is_Empty()
        {
            var request = RequestProductJsonBuilder.Build();
            request.ProductName = string.Empty;
            var useCase = CreateUseCase();
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await useCase.Execute(request));
            exception.Message.ShouldBe(ResourceErrorMessages.PRODUCT_NAME_EMPTY);
        }
        [Fact]
        public async Task Fail_When_ProductPrice_Is_Less_Than_Or_Equal_To_Zero()
        {
            var request = RequestProductJsonBuilder.Build();
            request.Price = 0;
            var useCase = CreateUseCase();
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await useCase.Execute(request));
            exception.Message.ShouldBe(ResourceErrorMessages.PRICE_GREATER_THAN_ZERO);
        }
        [Fact]
        public async Task ERROR_WHEN_PRODUCT_CATEGORY_IS_EMPTY()
        {
            var request = RequestProductJsonBuilder.Build();
            request.Category = string.Empty;
            var useCase = CreateUseCase();
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await useCase.Execute(request));
            exception.Message.ShouldBe(ResourceErrorMessages.PRODUCT_CATEGORY_EMPTY);
        }

        private RegisterProductUseCase CreateUseCase()
        {
            var repository = ProductsWriteOnlyRepositoryBuilder.Build();
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            

            return new RegisterProductUseCase(repository, unitOfWork, mapper);
        }
    }
}

