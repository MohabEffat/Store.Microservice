namespace Orders.BusinessLogic.Dtos
{
    public class AddOrUpdateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
