namespace ProvaPratica.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool Status { get; set; }

        public string? Image { get; set; }
    }
}
