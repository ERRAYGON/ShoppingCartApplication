using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShoppingCartApplication.CustomExceptions;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;
using ShoppingCartApplication.Repositories;
using ShoppingCartApplication.ServiceContract;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace ShoppingCartApplication.Service
{
    public class UserService : IUserService
    {
        public readonly IUserRepository UserRepository;
        public readonly IMapper Mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            UserRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
        }

        public async Task<bool> AuthenticateUser(UserLoginDTO userLoginDetails)
        {
            try
            {
                var user = await UserRepository.GetUserThroughEmailPassword(userLoginDetails) ?? 
                    throw new ItemNotFoundException("User", "User not found");
                if (user!.email == userLoginDetails.email &&
                    user.password == userLoginDetails.password)
                {
                    _httpContextAccessor.HttpContext!.Session.SetString("UserId", user.UserId.ToString());
                    return true;
                }
                else throw new UnauthorizedAccessException("Incorrect email or password");
            }
            catch { throw; }
        }

        public async Task<bool> DeleteUser(UserLoginDTO userLoginDetail)
        {
            try
            {
                var user = await UserRepository.GetUserThroughEmailPassword(userLoginDetail) ??
                   throw new ItemNotFoundException("User", "User not found");
                if (user!.email == userLoginDetail.email &&
                    user.password == userLoginDetail.password)
                {
                    await UserRepository.DeleteUserDetails(user.UserId);
                    return true;
                }
                else throw new UnauthorizedAccessException("Incorrect email or password");
            }
            catch { throw; }
        }

        public async Task<bool> RegisterUser(UserRegistrationDTO userDetails)
        {
            try
            {
                User user = Mapper.Map<User>(userDetails);

                //user.Name = userDetails.FirstName + " " + userDetails.LastName;

                UserType userType = Mapper.Map<UserType>(Mapper.Map<UserTypeDTO>(user));
                // The inner mapper would return an object of type UserTypeDTO that is then mapped with UserType

                bool isDuplicateEmail = await UserRepository.IsRegisteringWithDuplicateEmail(user);
                if (isDuplicateEmail)
                {
                    throw new BadHttpRequestException("Email address is already present. Register using different email Address");
                }
                return await UserRepository.AddUser(user, userType);
            }
            catch { throw; }
        }
    }
}
