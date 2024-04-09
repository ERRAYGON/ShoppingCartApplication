using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;

namespace ShoppingCartApplication.ServiceContract
{
    public interface IUserService
    {
        public Task<bool> RegisterUser(UserRegistrationDTO userDetails);
        public Task<bool> AuthenticateUser(UserLoginDTO userLoginDetails);
        public Task<bool> DeleteUser(UserLoginDTO userLoginDetail);
    }
}
