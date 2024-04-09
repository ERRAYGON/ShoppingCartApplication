using ShoppingCartApplication.Models;

namespace ShoppingCartApplication.Repositories
{
    public interface IOrderRepository
    {
        public Task<bool> PlaceOrder(Order order, Guid userId);
        public Task<double> GetOrderPrice(Order order);
        public Task<Order> GetOrderDetailsOfUser(Guid userId);
        public Task<User?> GetUserById(Guid userId);
    }
}
