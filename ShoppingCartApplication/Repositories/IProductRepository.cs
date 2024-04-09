using ShoppingCartApplication.Models;

namespace ShoppingCartApplication.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Products>> GetAllProducts();
        public Task<bool> AddProduct(Products product);
        public Task<Products?> GetProductByProductName(string productName);
        public Task<bool> DeleteProduct(Products product);
        public Task<User?> GetUserById(Guid userId);
        public Task<UserType?> GetUserTypeById(Guid userId);
        public Task<Products?> GetProductByProductId(Guid productId);

    }
}
