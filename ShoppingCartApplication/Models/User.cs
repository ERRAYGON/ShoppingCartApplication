using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [EmailAddress]
        
        public string email { get; set; }

        [MinLength(5)]
        [MaxLength(10)]
        public string password { get; set; }
    }
}
