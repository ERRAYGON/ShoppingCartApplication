namespace ShoppingCartApplication.Models.DTO
{
    public class CartDetailDTO
    {
        public List<ProductDetailDTO> products { get; set; }
        public double TotalPrice { get; set; }
    }
}
