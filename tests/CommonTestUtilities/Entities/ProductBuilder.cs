using Bogus;
using ProvaPratica.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class ProductBuilder
    {
        public static List<Product> Collection(uint count = 2)
        {
            var list = new List<Product>();

            if (count == 0)
                count = 1;




            for (int i = 0; i < count; i++)
            {
                var product = Build();
                product.Id = i;

                list.Add(product);
            }

            return list;
        }

        public static Product Build()
        {
            return new Faker<Product>()
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Status, f => f.Random.Bool())
                .RuleFor(p => p.Image, f => f.Image.PicsumUrl());
                
        }
    }
}
