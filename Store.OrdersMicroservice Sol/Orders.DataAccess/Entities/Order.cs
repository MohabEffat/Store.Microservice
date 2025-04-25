using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Orders.DataAccess.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders.DataAccess.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [BsonRepresentation(BsonType.String)]
        public string UserEmail { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public ShippingAddress ShippingAddress { get; set; } 
        public DeliveryMethod DeliveryMethod { get; set; }
        [BsonIgnoreIfNull]
        public Guid? DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.placed;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        [BsonRepresentation(BsonType.Decimal128)]
        public double SubTotal { get; set; }
        public double GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
