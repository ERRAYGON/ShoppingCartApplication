using Microsoft.EntityFrameworkCore;
using ShoppingCartApplication.Data;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;

namespace ShoppingCartApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ShoppingCartDbContext DbContext { get; set; }
        public UserRepository(ShoppingCartDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<bool> AddUser(User user, UserType userType)
        {
            try
            {
                //user.Name = user.Name.Substring(0, user.Name.IndexOf(" ") + 1) + " " + user.Name.Substring(user.Name.IndexOf(" ") + 1);

                var AddedUser = await DbContext.User.AddAsync(user);
                var AddedUserType = await DbContext.UserRole.AddAsync(userType);
                DbContext.SaveChanges();

                return AddedUser.IsKeySet && AddedUserType.IsKeySet;
            }
            catch { throw; }
        }

        public async Task<User?> GetUserThroughEmailPassword(UserLoginDTO userLoginDetails)
        {
            try
            {
                User? foundUser = await DbContext.User.
                    FirstOrDefaultAsync(user => user.email == userLoginDetails.email);
                return foundUser;
            }
            catch { throw; }
        }

        public async Task<bool> IsRegisteringWithDuplicateEmail(User userDetails)
        {
            try
            {
                User? foundUser = await DbContext.User.
                    FirstOrDefaultAsync(user => user.email == userDetails.email);
                if (foundUser == null) return false;
                return true;
            }
            catch { throw; }
        }

        public async Task<bool> DeleteUserDetails(Guid userId)
        {
            try
            {
                DbContext.Cart.RemoveRange(DbContext.Cart.Where(cart => cart.UserId == userId));
                DbContext.Order.RemoveRange(DbContext.Order.Where(order => order.UserId == userId));
                DbContext.UserRole.RemoveRange(DbContext.UserRole.Where(userRole => userRole.UserId == userId));
                DbContext.User.RemoveRange(DbContext.User.Where(user => user.UserId == userId));
                DbContext.SaveChanges();
                return true;
            } catch { throw; }
        }
    }
}
