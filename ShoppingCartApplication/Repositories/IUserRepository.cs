using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;

namespace ShoppingCartApplication.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> AddUser(User user, UserType userType);
        public Task<User?> GetUserThroughEmailPassword(UserLoginDTO userLoginDetails);
        public Task<bool> IsRegisteringWithDuplicateEmail(User userDetails);
        public Task<bool> DeleteUserDetails(Guid userId);
    }
}
