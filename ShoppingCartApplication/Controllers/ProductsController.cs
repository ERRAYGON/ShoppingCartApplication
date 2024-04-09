using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;
using ShoppingCartApplication.Service;
using ShoppingCartApplication.ServiceContract;
using System.Net;

namespace ShoppingCartApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService ProductService;
        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts() // https://localhost:1234/api/cart/
        {
            try
            {
                List<Products> productsList = await ProductService.GetAllProduct();
                return Ok(productsList);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDetails)
        {
            try
            {
                await ProductService.AddProduct(productDetails);
                return Ok("Product Added");
            }
            catch(UnauthorizedAccessException ex)
            {
                    return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("deleteproduct")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                await ProductService.DeleteProduct(productId);
                return Ok("Product Deleted");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
