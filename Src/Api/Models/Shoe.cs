namespace Api.Models
{
    public class Shoe
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public Retailer Retailer { get; set; }
    }
}
