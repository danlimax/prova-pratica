using FluentValidation;
using ProvaPratica.Communication.Requests;
using ProvaPratica.Exception;

namespace ProvaPratica.Application.Products
{
    public class ProductValidator : AbstractValidator<RequestProductJson>
    {
        public ProductValidator() {
            RuleFor(product => product.ProductName).NotEmpty().WithMessage(ResourceErrorMessages.PRODUCT_NAME_EMPTY);
            RuleFor(product => product.Category).NotEmpty().WithMessage(ResourceErrorMessages.PRODUCT_CATEGORY_EMPTY);
            RuleFor(product => product.Price).GreaterThan(0).WithMessage(ResourceErrorMessages.PRICE_GREATER_THAN_ZERO);
        }
    }
}
