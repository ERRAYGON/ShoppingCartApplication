using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models
{
    public class Products
    {
        [Key]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
