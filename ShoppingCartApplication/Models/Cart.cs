using ShoppingCartApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    [Key]
    public Guid CartId { get; set; }

    public Guid UserId { get; set; }
    public Guid ProductId { get; set; } // Change this to Guid

    //Navigation Property
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [ForeignKey("ProductId")]
    public virtual Products Product { get; set; }
}
