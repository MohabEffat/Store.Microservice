using Orders.BusinessLogic.Dtos;
using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Orders.BusinessLogic.HttpClients
{
    public class ProductMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<ProductMicroserviceClient> _logger;

        public ProductMicroserviceClient(HttpClient httpClient,
            IDistributedCache cache,
            ILogger<ProductMicroserviceClient> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
        }

        public async Task<ProductDto?> GetProductById(Guid productId)
        {
            var cacheKey = $"Product:{productId}";
            _logger.LogInformation("Fetching product with ID: {ProductId}", productId);

            var cachedProduct = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedProduct))
            {
                _logger.LogInformation("Product retrieved from cache: {ProductId}", productId);
                return JsonSerializer.Deserialize<ProductDto>(cachedProduct);
            }

            _logger.LogInformation("Cache miss. Fetching product from API: {ProductId}", productId);
            var response = await _httpClient.GetAsync($"/api/Product/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch product {ProductId}. Status Code: {StatusCode}", productId, response.StatusCode);

                return response.StatusCode switch
                {
                    HttpStatusCode.NotFound => null,
                    HttpStatusCode.BadRequest => throw new HttpRequestException("BadRequest", null, HttpStatusCode.BadRequest),
                    _ => throw new HttpRequestException($"Http Request Failed With Status Code {response.StatusCode}")
                };
            }

            var product = await response.Content.ReadFromJsonAsync<ProductDto>();

            if (product == null)
            {
                _logger.LogWarning("Product not found in API response: {ProductId}", productId);
                return null;
            }

            _logger.LogInformation("Product retrieved successfully from API: {ProductId}", productId);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(product), cacheOptions);
            _logger.LogInformation("Product cached: {ProductId}", productId);

            return product;
        }
    }
}
