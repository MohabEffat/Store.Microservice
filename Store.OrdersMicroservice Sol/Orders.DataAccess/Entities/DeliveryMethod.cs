using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

public class DeliveryMethod
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ShortName { get; set; }
    public string Description { get; set; }
    [BsonRepresentation(BsonType.String)]
    public string DeliveryTime { get; set; }
    [BsonRepresentation(BsonType.Decimal128)]
    public double Price { get; set; }
}
