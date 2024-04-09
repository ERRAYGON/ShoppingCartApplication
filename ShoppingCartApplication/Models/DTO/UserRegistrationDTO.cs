using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApplication.Models.DTO
{
    public class UserRegistrationDTO
    {
        public Guid UserId { get; }

        [Required]
        [MinLength(3, ErrorMessage = "Name should have minimum length of 3")]
        [MaxLength(40, ErrorMessage = "Name should have maximum length of 40")]
        [RegularExpression(@"^[a-zA-Z]+\s*[a-zA-Z]*$", ErrorMessage = "Name should be in 'FirstName LastName' format. LastName is optional.")]
        public string Name { get; set; }
        //[Required]
        //[MinLength(3, ErrorMessage = "FirstName should have minimum length of 3")]
        //[MaxLength(20, ErrorMessage = "LastName should have maximum length of 20")]
        //[RegularExpression(@"^[a-zA-Z]$", ErrorMessage = "Name must contains Alphabets only")]
        //public string FirstName { get; set; }

        //[Required]
        //[MinLength(3, ErrorMessage = "Last Name should have minimum length of 3")]
        //[MaxLength(20, ErrorMessage = "Last Name should have maximum length of 20")]
        //[RegularExpression(@"^*[a-zA-Z]*$", ErrorMessage = "Name must contains Alphabets only")]
        //public string LastName { get; set; }


        [EmailAddress]
        [Required]
        public string email {get; set;}

        [Required]
        [MinLength(5, ErrorMessage = " Password should be greater than 5 characters")]
        [MaxLength(10, ErrorMessage = " Password should be less than 10 characters")]
        //[RegularExpression(@"^[a-zA-Z@#]$", ErrorMessage = "Password should only contain Alphabets and @, # as special characters")]
        public string password { get; set; }

        public UserRegistrationDTO()
        {
            UserId = Guid.NewGuid();
        }
    }
}
