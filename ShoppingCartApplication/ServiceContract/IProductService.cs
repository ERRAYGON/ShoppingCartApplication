using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;

namespace ShoppingCartApplication.ServiceContract
{
    public interface IProductService
    {
        public Task<List<Products>> GetAllProduct();
        public Task<bool> AddProduct(ProductDTO productDetails);
        public Task<bool> DeleteProduct(Guid productId);
        public Task<bool> CheckIfAdmin();
    }
}
