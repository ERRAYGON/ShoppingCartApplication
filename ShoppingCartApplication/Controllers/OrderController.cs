using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplication.CustomExceptions;
using ShoppingCartApplication.ServiceContract;
using System.Net;

namespace ShoppingCartApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        public OrderController(IOrderService orderService)
        {
            this.OrderService = orderService;
        }

        [HttpGet]
        [Route("orderdetails")]
        public async Task<IActionResult> GetOrderDetails()
        {
            try
            {
                var orderDetails = await OrderService.GetOrderDetails();
                return Ok(orderDetails);
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex) 
            { 
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); 
            }
        }

        [HttpGet]
        [Route("order")]
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                Order order = await OrderService.PlaceOrder();
                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
