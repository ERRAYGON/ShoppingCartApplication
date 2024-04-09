using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models.DTO
{
    public class ProductDetailDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductQuantity { get; set; }
    }
}
