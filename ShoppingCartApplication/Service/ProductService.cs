using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Identity.Client;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;
using ShoppingCartApplication.Repositories;
using ShoppingCartApplication.ServiceContract;

namespace ShoppingCartApplication.Service
{
    public class ProductService : IProductService
    {

        public readonly IProductRepository ProductRepository;
        public readonly IMapper Mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            ProductRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
        }


        private async Task<User> GetUser()
        {
            string? userId = _httpContextAccessor.HttpContext!.Session.GetString("UserId");
            if (userId == null || ProductRepository.GetUserById(Guid.Parse(userId)) == null) 
                throw new UnauthorizedAccessException("User is not logged in");

            return await ProductRepository.GetUserById(Guid.Parse(userId));
        }

        private async Task<UserType> GetUserType(User user)
        {
            try
            {
                UserType? userType = await ProductRepository.GetUserTypeById(user.UserId);
                return userType ?? throw new UnauthorizedAccessException("User doesn't have a role");
            }
            catch { throw; }
        }

        public async Task<bool> CheckIfAdmin()
        {
            try
            {
                var user = await GetUser();

                var userType = await GetUserType(user!);
                if (userType.UserRole == "Admin") return true;
                else throw new UnauthorizedAccessException("User is not an Admin");
            } catch { throw; }
        }

        public async Task<List<Products>> GetAllProduct()
        {
            try
            {
                return await ProductRepository.GetAllProducts();
            } catch { throw; }
        }

        public async Task<bool> AddProduct(ProductDTO productDetails)
        {
            try
            {
                await CheckIfAdmin();
                Products product = Mapper.Map<Products>(productDetails);
                product.ProductId = Guid.NewGuid();
                return await ProductRepository.AddProduct(product);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                await CheckIfAdmin();
                Products? product = await ProductRepository.GetProductByProductId(productId);
                if (product == null) return false;
                return await ProductRepository.DeleteProduct(product);
            } 
            catch { throw; }
        }
    }
}
