using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartApplication.CustomExceptions;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Repositories;
using ShoppingCartApplication.ServiceContract;

namespace ShoppingCartApplication.Service
{

    public class OrderService : IOrderService
    {
        public readonly IOrderRepository OrderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
        {
            OrderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<User?> GetUser()
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext!.Session.GetString("UserId");
                if (userId == null) return null;


                var user = await OrderRepository.GetUserById(Guid.Parse(userId));
                return user;
            } catch { throw; };
        }

        public async Task<Order?> GetOrderDetails()
        {
            try
            {
                User? user = await GetUser() ?? throw new UnauthorizedAccessException("User is not loggedIn");
                var order = await OrderRepository.GetOrderDetailsOfUser(user.UserId);
                return order;
            }
            catch { throw; };
        }
        public async Task<Order> PlaceOrder()
        {
            try { 
                User? user = await GetUser() ?? throw new UnauthorizedAccessException("User is not logged in");
                Order order = new()
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    UserId = user.UserId,
                    OrderStatus = "Preparing for shipment"
                };
                order.OrderPrice = await OrderRepository.GetOrderPrice(order);

                if (order.OrderPrice == 0) throw new ItemNotFoundException("Cart", "Cart is Empty");
                bool isOrderPlaced = await OrderRepository.PlaceOrder(order, user.UserId);
                if (isOrderPlaced) return order;
                else throw new InvalidOperationException("Something went wrong. Order not placed. Please try again");
            }
            catch { throw; };
        }
    }
}
