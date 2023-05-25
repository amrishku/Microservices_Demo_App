using System.ComponentModel.DataAnnotations;

namespace Chingari.Services.OrderAPI.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }
}
