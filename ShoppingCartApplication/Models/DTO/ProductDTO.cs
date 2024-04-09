using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models.DTO
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Product Name cannot be empty")]
        [MinLength(3, ErrorMessage = "Product Name should have minimum length of 3")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Price cannot be empty")]
        public double ProductPrice { get; set; }
    }
}
