using Microsoft.EntityFrameworkCore;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace ShoppingCartApplication.Repositories
{
    public class CartRepository : ICartRepository
    {
        public ShoppingCartDbContext DbContext { get; set; }
        public CartRepository(ShoppingCartDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public List<Cart> GetAllCartItemsOfUser(Guid userId)
        {
            try
            {
                var cart = DbContext.Cart.Where(cartItem => cartItem.UserId == userId).ToList();
                return cart;
            } catch { throw; }
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            try
            {
                var user = await DbContext.User.FindAsync(userId);
                return user;
            } catch { throw; }
        }

        public async Task<Products?> GetProductById(Guid productId)
        {
            try {
                var product = await DbContext.Products.FirstOrDefaultAsync(product => product.ProductId == productId);
                return product;
            }
            catch { throw; }
        }

        public async Task<bool> AddItemsInCart(Cart cartItem)
        {
            try
            {
                var cartItemAdded = await DbContext.Cart.AddAsync(cartItem);
                await DbContext.SaveChangesAsync();
                return cartItemAdded.IsKeySet;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCartItemsOfUser(Guid id, Guid userId)
        {
            try
            {
                var cartItem = DbContext.Cart.FirstOrDefault(item => item.UserId == userId && item.ProductId == id);
                if (cartItem == null) return false;
                else
                {
                    var productRemoved = DbContext.Cart.Remove(cartItem);
                    await DbContext.SaveChangesAsync();
                    return productRemoved.IsKeySet;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
