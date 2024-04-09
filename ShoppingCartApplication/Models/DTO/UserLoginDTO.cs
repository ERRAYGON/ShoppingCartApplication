using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models.DTO
{
    public class UserLoginDTO
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string password { get; set; }
    }
}
