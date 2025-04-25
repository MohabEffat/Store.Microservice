using Orders.BusinessLogic.Dtos;

namespace Orders.BusinessLogic.ServicesContracts
{
    public interface IOrderService
    {
        Task<OrderDto?> CreateOrder(AddOrderDto addOrder);
        Task<IEnumerable<OrderDto?>> GetOrders();
        Task<OrderDto?> UpdateOrder(UpdateOrderDto updateOrder);
        Task<OrderDto?> GetOrderById(Guid orderId);
        Task<bool> DeleteOrder(Guid orderId);
        Task<IEnumerable<OrderDto>?> GetOrderByBuyerEmail(string Email);

    }
}