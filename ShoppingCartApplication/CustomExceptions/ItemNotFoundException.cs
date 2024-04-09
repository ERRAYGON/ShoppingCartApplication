using System.Runtime.Serialization;

namespace ShoppingCartApplication.CustomExceptions
{
    public class ItemNotFoundException : Exception
    {
        public readonly string? ItemType = "Item";
        public new readonly string? Message;
        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string? message) : base(message)
        {
        }

        public ItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ItemNotFoundException(string itemType, string message)
        {
            ItemType = itemType;
            Message = message;
        }
    }
}
