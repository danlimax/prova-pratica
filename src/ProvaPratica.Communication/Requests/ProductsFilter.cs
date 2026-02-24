namespace ProvaPratica.Communication.Requests
{
    public class ProductsFilter
    {
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? Status { get; set; }

        public string? Image { get; set; } 
    }
}
