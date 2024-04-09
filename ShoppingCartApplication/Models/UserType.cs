using ShoppingCartApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class UserType
{
    [Key]
    public Guid UserTypeId { get; set; }

    public string UserRole { get; set; }

    public Guid UserId { get; set; }

    //Navigation Property
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}