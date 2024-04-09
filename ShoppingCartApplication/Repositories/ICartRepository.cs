using ShoppingCartApplication.Models;

namespace ShoppingCartApplication.Repositories
{
    public interface ICartRepository
    {
        public List<Cart> GetAllCartItemsOfUser(Guid userId);
        public Task<User?> GetUserById(Guid userId);
        public Task<bool> AddItemsInCart(Cart cart);
        public Task<Products?> GetProductById(Guid productId);
        public Task<bool> DeleteCartItemsOfUser(Guid id, Guid userId);

    }
}
