using Orders.DataAccess.Entities;
using Orders.DataAccess.RepositoriesContracts;
using Orders.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Orders.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        //private readonly IMongoCollection<Order> _orders;
        //private readonly string CollectionName = "Orders";
        //public OrderRepository(IMongoDatabase mongoDatabase)
        //{
        //    _orders = mongoDatabase.GetCollection<Order>(CollectionName);
        //}
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order is null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByBuyerEmailAsync(string email)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Include(x => x.DeliveryMethod)
                .Where(x => x.UserEmail == email).ToListAsync();
        }

        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);

            _context.Entry(existingOrder!).CurrentValues.SetValues(order);

            await _context.SaveChangesAsync();

            return existingOrder;
        }
        #region Mongo
        //public async Task<Order?> AddOrder(Order order)
        //{
        //    order.Id = Guid.NewGuid();

        //    foreach (var orderItem in order.OrderItems)
        //    {
        //        orderItem.OrderItemId = Guid.NewGuid();
        //    }
        //    await _orders.InsertOneAsync(order);
        //    return order;
        //}

        //public async Task<bool> DeleteOrder(Guid orderId)
        //{
        //    var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);

        //    var existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

        //    if (existingOrder == null) return false;

        //    var result = await _orders.DeleteOneAsync(filter);

        //    return result.DeletedCount > 0;

        //}

        //public async Task<Order?> GetOrderByConditions(FilterDefinition<Order> filter)
        //{
        //    return (await _orders.FindAsync(filter)).FirstOrDefault();
        //}

        //public async Task<IEnumerable<Order>> GetOrders()
        //{
        //    return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
        //}

        //public async Task<IEnumerable<Order?>> GetOrdersByConditions(FilterDefinition<Order> filter)
        //{
        //    return (await _orders.FindAsync(filter)).ToList();
        //}

        //public async Task<Order?> UpdateOrder(Order order)
        //{
        //    var filter = Builders<Order>.Filter.Eq(x => x.Id, order.Id);

        //    var existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

        //    if (existingOrder == null) return null;

        //    await _orders.ReplaceOneAsync(filter, order);

        //    return order;
        //} 
        #endregion


    }
}
