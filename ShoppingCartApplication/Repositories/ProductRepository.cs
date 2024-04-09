using Microsoft.EntityFrameworkCore;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Models;
using System.Linq;

namespace ShoppingCartApplication.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ShoppingCartDbContext DbContext { get; set; }
        public ProductRepository(ShoppingCartDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<List<Products>> GetAllProducts()
        {
            try
            {
                //return DbContext.Products.ToList();
                return DbContext.GetAllProducts();
            } catch { throw; }
        }

        public async Task<bool> AddProduct(Products product)
        {
            try
            {
                var productAdded = await DbContext.Products.AddAsync(product);
                //product.ProductName = product.ProductName.Substring(0, product.ProductName.IndexOf(" " + 2));

                //var productAdded = DbContext.AddProduct(product);
                DbContext.SaveChanges();
                return productAdded != null;
            }
            catch {
                throw;
            }
        }
        public async Task<Products?> GetProductByProductName(string productName)
        {
            try
            {
                var product = await DbContext.Products.FirstOrDefaultAsync(product => product.ProductName == productName);
                return product;
            }
            catch { throw; }
        }
        public async Task<Products?> GetProductByProductId(Guid productId)
        {
            try
            {
                var product = await DbContext.Products.FindAsync(productId);
                return product;
            }
            catch { throw; }
        }
        public async Task<User?> GetUserById(Guid userId)
        {
            try
            {
                var user = await DbContext.User.FindAsync(userId);
                return user;
            }
            catch { throw; }
        }
        public async Task<UserType?> GetUserTypeById(Guid userId)
        {
            try
            {
                var userRole = DbContext.UserRole.FirstOrDefault(userRole => userRole.UserId == userId);
                return userRole;
            }
            catch { throw; }
        }

        public async Task<bool> DeleteProduct(Products product)
        {
            try
            {
                var productRemoved = DbContext.Products.Remove(product);
                await DbContext.SaveChangesAsync();
                return productRemoved.IsKeySet;
            }
            catch { throw; }
        }
    }
}
