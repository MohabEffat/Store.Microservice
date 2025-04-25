using Orders.DataAccess.Entities;

namespace Orders.BusinessLogic.Dtos
{
    public class AddOrderDto
    {
        public string UserEmail { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public ICollection<AddOrUpdateOrderItemDto> OrderItems { get; set; }
            = new HashSet<AddOrUpdateOrderItemDto>();

    }
}
