using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Orders.DataAccess.Entities
{
    public class OrderItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [Key]
        public Guid OrderItemId { get; set; } = Guid.NewGuid();
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public double UnitPrice { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
        [BsonIgnore] // Calculated dynamically instead of storing
        public double TotalPrice => UnitPrice * Quantity;
    }
}
