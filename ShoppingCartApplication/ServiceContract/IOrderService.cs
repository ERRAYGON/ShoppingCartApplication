namespace ShoppingCartApplication.ServiceContract
{
    public interface IOrderService
    {
        public Task<Order?> GetOrderDetails();
        public Task<Order> PlaceOrder();
    }
}
