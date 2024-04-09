using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;

namespace ShoppingCartApplication.ServiceContract
{
    public interface ICartService
    {
        public Task<CartDetailDTO> GetAllCartItems();
        public Task<Products> AddItemInCart(Guid productId);
        public Task<bool> DeleteCartItem(Guid id);

    }
}
