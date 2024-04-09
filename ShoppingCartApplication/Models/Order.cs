using ShoppingCartApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public double OrderPrice { get; set; }
    public string OrderStatus { get; set; }

    public Guid UserId { get; set; }

    //Navigation Property
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}