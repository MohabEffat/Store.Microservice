namespace Orders.BusinessLogic.Dtos
{
    public class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public ICollection<AddOrUpdateOrderItemDto> OrderItems { get; set; }
            = new HashSet<AddOrUpdateOrderItemDto>();
    }
}
