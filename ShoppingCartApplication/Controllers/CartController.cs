using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplication.CustomExceptions;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.ServiceContract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ShoppingCartApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartService CartService;
        public CartController(ICartService cartService)
        {
            this.CartService = cartService;
        }


        [HttpGet]
        [Route("")]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> ListCartItems()
        {
            try
            {
                var cartItems = await CartService.GetAllCartItems();
                if (cartItems == null) // If list is null that means, user is not authenticated
                    return Unauthorized("User is not logged in");
                else if (cartItems.products.Count == 0) // If list is empty, cart is empty
                    return Ok("Cart is Empty!!");


                return Ok(cartItems);
            }

            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(((int)HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [HttpPost]
        [Route("additem")]
        public async Task<IActionResult> AddItemInCart([FromBody] Guid id)
        {
            try
            {
                await CartService.AddItemInCart(id);
                return Ok("Product added");
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(((int)HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteitem")]
        public async Task<IActionResult> DeleteCartItem([FromBody] Guid id)
        {
            try
            {
                await CartService.DeleteCartItem(id);
                return Ok("Product deleted");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(((int)HttpStatusCode.InternalServerError), ex.InnerException + "\n" + ex.Message);
            }
        }
    }
}
