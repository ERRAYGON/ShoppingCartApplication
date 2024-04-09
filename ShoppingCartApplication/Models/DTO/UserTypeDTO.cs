using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models.DTO
{
    public class UserTypeDTO
    {
        public Guid UserTypeId { get;}

        [Required]
        public string UserRole { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }
        public UserTypeDTO() { 
            UserTypeId = Guid.NewGuid();
            UserRole = "Customer";
        }
    }
}
