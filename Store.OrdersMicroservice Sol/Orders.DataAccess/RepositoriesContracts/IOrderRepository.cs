using Orders.DataAccess.Entities;
using MongoDB.Driver;

namespace Orders.DataAccess.RepositoriesContracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByBuyerEmailAsync(string email);
        //Task<IEnumerable<Order?>> GetOrdersByConditions(FilterDefinition<Order> filter);
        //Task<Order?> GetOrderById(FilterDefinition<Order> filter);
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<Order?> AddOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
