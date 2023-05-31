namespace Chingari.Web.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}
