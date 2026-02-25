using Bogus;
using ProvaPratica.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestProductJsonBuilder
    {
        public static RequestProductJson Build()
        {
            return new Faker<RequestProductJson>()
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Status, f => f.Random.Bool());
                
        }
    }
}
