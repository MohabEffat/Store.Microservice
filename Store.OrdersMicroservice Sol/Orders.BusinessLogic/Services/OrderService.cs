using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Orders.BusinessLogic.Dtos;
using Orders.BusinessLogic.HttpClients;
using Orders.BusinessLogic.ServicesContracts;
using Orders.DataAccess.Entities;
using Orders.DataAccess.RepositoriesContracts;
using System.Text.Json;

namespace Orders.BusinessLogic.Services
{
    public class OrderService : IOrderService
    { 
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly UserMicroserviceClient _userClient;
        private readonly ProductMicroserviceClient _productClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IOrderRepository orderRepository,
            IMapper mapper, UserMicroserviceClient userClient,
            ProductMicroserviceClient productClient, IDistributedCache cache, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userClient = userClient;
            _productClient = productClient;
            _cache = cache;
            _logger = logger;
        }
        public async Task<OrderDto?> CreateOrder(AddOrderDto? addOrder)
        {
            _logger.LogInformation("Creating new order for user: {UserEmail}", addOrder?.UserEmail);

            if (addOrder?.OrderItems == null || !addOrder.OrderItems.Any())
                throw new ArgumentException("Order must contain at least one item.", nameof(addOrder));

            var user = await _userClient.GetUserByEmail(addOrder.UserEmail)
                        ?? throw new InvalidOperationException("Invalid User Email");

            var orderItemsList = new List<OrderItemDto>();

            foreach (var orderItem in addOrder.OrderItems)
            {
                var product = await _productClient.GetProductById(orderItem.ProductId)
                              ?? throw new KeyNotFoundException($"Product with ID {orderItem.ProductId} does not exist.");

                orderItemsList.Add(new OrderItemDto
                {
                    ProductId = orderItem.ProductId,
                    Description = product.Description,
                    Name = product.Name,
                    UnitPrice = product.UnitPrice,
                    Quantity = orderItem.Quantity
                });
            }

            var orderInput = _mapper.Map<Order>(addOrder);
            orderInput.OrderItems = _mapper.Map<List<OrderItem>>(orderItemsList);
            orderInput.SubTotal = orderInput.OrderItems.Sum(x => x.TotalPrice);

            var addedOrder = await _orderRepository.AddOrderAsync(orderInput);
            if (addedOrder == null) return null;

            var mappedOrder = _mapper.Map<OrderDto>(addedOrder);

            mappedOrder.OrderItems = orderItemsList;

            _logger.LogInformation("Order successfully created with ID: {OrderId}", addedOrder?.Id);

            return mappedOrder; 

        }
        public async Task<bool> DeleteOrder(Guid orderId)
        {
            _logger.LogInformation("Deleting order with ID: {OrderId}", orderId);
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
        public async Task<IEnumerable<OrderDto>?> GetOrderByBuyerEmail(string Email)
        {
            _logger.LogInformation("Retrieving orders for buyer email: {Email}", Email);

            var cacheKey = $"Email:{Email}";
            var cachedOrders = await _cache.GetStringAsync(cacheKey);
            if (cachedOrders != null)
            {
                _logger.LogInformation("Orders retrieved from cache for email: {Email}", Email);
                return JsonSerializer.Deserialize<IEnumerable<OrderDto>>(cachedOrders);

            }

            var orders = (await _orderRepository.GetOrdersByBuyerEmailAsync(Email))?.ToList();
            if (orders == null || !orders.Any())
            {
                _logger.LogWarning("No orders found for email: {Email}", Email);
                return null;
            }

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(orders), cacheOptions);

            _logger.LogInformation("Orders cached for email: {Email}", Email);

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderById(Guid orderId)
        {
            _logger.LogInformation("Retrieving order with ID: {OrderId}", orderId);
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            { 
                _logger.LogInformation("No Orders Found : {OrderId}", orderId);
                return null;
            }
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto?>> GetOrders()
        {
            _logger.LogInformation("Retrieving all orders");
            var orders = await _orderRepository.GetOrdersAsync();
            var mappedOrders = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderDto?> UpdateOrder(UpdateOrderDto updateOrder)
        {
            _logger.LogInformation("Updating order with ID: {OrderId}", updateOrder?.Id);

            var order = _mapper.Map<Order>(updateOrder);
            if (order == null) return null;
            var existingOrder = await _orderRepository.UpdateOrderAsync(order);
            _logger.LogInformation("Order With : {OrderId} Updated Successfully", updateOrder?.Id);

            return _mapper.Map<OrderDto>(existingOrder);
        }
    }
}
