using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Models;

namespace ShoppingCartApplication.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public ShoppingCartDbContext DbContext { get; set; }
        public OrderRepository(ShoppingCartDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            try
            {
                var user = await DbContext.User.FindAsync(userId);
                return user;
            } catch { throw; }
        }

        public async Task<Order?> GetOrderDetailsOfUser(Guid userId)
        {
            try
            {
                var isOrderPlaced = DbContext.Order.FirstOrDefault(order => order.UserId == userId);
                return isOrderPlaced;
            }
            catch { throw; }
        }
        public async Task<bool> PlaceOrder(Order order, Guid userId)
        {
            try
            {
                var isOrderPlaced = await DbContext.Order.AddAsync(order);
                await DbContext.SaveChangesAsync();
                if (isOrderPlaced.IsKeySet)
                {
                    List<Cart> cartItems = DbContext.Cart.Where(cartItem => cartItem.UserId == userId).ToList();
                    DbContext.Cart.RemoveRange(cartItems);
                    await DbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch { throw; }
        }
        public async Task<double> GetOrderPrice(Order order)
        {
            try
            {
                var cartItems = DbContext.Cart.Where(cartItems => cartItems.UserId == order.UserId).ToList();
                double sum = cartItems.Sum(cartItem => DbContext.Products.Find(cartItem.ProductId)!.ProductPrice);
                return sum;
            } catch { throw; }
        }
    }
}
