using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShoppingCartApplication.Models;
using System.Threading.Tasks;

namespace ShoppingCartApplication.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserRole { get; set; }

        public List<Products> GetAllProducts()
        {
            return Products.FromSqlRaw("EXECUTE [dbo].[GetAllProducts]").ToList();
        }
        public Products? AddProduct(Products product)
        {
            return Products.FromSqlRaw("EXECUTE [dbo].[AddProduct] @ProductId, @ProductName, @ProductPrice",
                new SqlParameter("ProductId", product.ProductId),
                new SqlParameter("ProductName", product.ProductName),
                new SqlParameter("ProductPrice", product.ProductPrice)).FirstOrDefault();
        }
    }
}
