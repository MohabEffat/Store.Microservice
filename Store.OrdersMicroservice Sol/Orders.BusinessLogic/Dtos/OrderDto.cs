using Orders.DataAccess.Entities;

namespace Orders.BusinessLogic.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethodDto DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
        public string UserEmail { get; set; }
        public decimal SubTotal { get; set; }

    }
}
