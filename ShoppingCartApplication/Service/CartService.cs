using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShoppingCartApplication.CustomExceptions;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;
using ShoppingCartApplication.Repositories;
using ShoppingCartApplication.ServiceContract;
using System.Reflection.Metadata.Ecma335;

namespace ShoppingCartApplication.Service
{
    public class CartService : ICartService
    {
        public readonly ICartRepository CartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartService(IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository)
        {
            CartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartDetailDTO> GetAllCartItems()
        {
            try
            {
                User? user = await GetUser();
                if (user != null)
                {
                    CartDetailDTO cartItems = new();
                    cartItems.products = new();
                    List<Cart> cart = CartRepository.GetAllCartItemsOfUser(user!.UserId);
                    foreach (var prd in cart)
                    {

                        Products? prod = await CartRepository.GetProductById(prd.ProductId);
                        if (prod == null) continue;
                        else if (cartItems.products.Select(product => product.ProductId).
                                                    FirstOrDefault(productId => productId.Equals(prod.ProductId)) == Guid.Empty)
                        {

                            ProductDetailDTO prdtl = new ProductDetailDTO()
                            {
                                ProductId = prod.ProductId,
                                ProductName = prod.ProductName,
                                ProductPrice = prod.ProductPrice,
                                ProductQuantity = 1
                            };
                            cartItems.products.Add(prdtl);
                        }
                        else
                        {
                            Guid foundProduct = cartItems.products.Select(product => product.ProductId).
                                                    FirstOrDefault(productId => productId.Equals(prod.ProductId));
                            cartItems.products.FirstOrDefault(product => product.ProductId == foundProduct)!.ProductQuantity++;
                        }
                    }
                    double sum = 0;
                    foreach (var product in cartItems.products)
                    {
                        sum += product.ProductPrice * product.ProductQuantity;
                    }
                    cartItems.TotalPrice = sum;
                    return cartItems;
                }
                else
                    throw new UnauthorizedAccessException("User is not loggedin");
            }
            catch
            {
                throw;
            }
        }

        public async Task<Products> AddItemInCart(Guid productId)
        {
            try
            {
                User? user = await GetUser() ?? throw new UnauthorizedAccessException("User is not loggedin");

                Products? product = await CartRepository.GetProductById(productId)
                    ??
                    throw new ItemNotFoundException("Product", "Product not found");
                Cart cartItem = new()
                {
                    ProductId = product.ProductId,
                    CartId = Guid.NewGuid(),
                    User = user,
                    Product = product,
                    UserId = user.UserId,
                };

                if (await CartRepository.AddItemsInCart(cartItem))
                    return product;
                else
                    throw new InvalidOperationException();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCartItem(Guid id)
        {
            try
            {
                User? user = await GetUser();
                if (user != null)
                {
                    if (!await CartRepository.DeleteCartItemsOfUser(id, user.UserId))
                        throw new InvalidOperationException("Something went wrong");
                    else return true;
                }
                else
                    throw new UnauthorizedAccessException("User is not loggedin");
            }
            catch
            {
                throw;
            }
        }
        
        private async Task<User?> GetUser()
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext!.Session.GetString("UserId");
                if (userId == null) return null;


                var user = await CartRepository.GetUserById(Guid.Parse(userId));
                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}
