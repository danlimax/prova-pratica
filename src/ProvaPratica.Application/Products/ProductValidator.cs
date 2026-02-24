using FluentValidation;
using ProvaPratica.Communication.Requests;

namespace ProvaPratica.Application.Products
{
    public class ProductValidator : AbstractValidator<RequestProductJson>
    {
        public ProductValidator() {
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("");
        }
    }
}
