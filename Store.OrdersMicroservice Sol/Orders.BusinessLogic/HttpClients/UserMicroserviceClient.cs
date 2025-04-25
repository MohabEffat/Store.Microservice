using Orders.BusinessLogic.Dtos;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace Orders.BusinessLogic.HttpClients
{
    public class UserMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserMicroserviceClient> _logger;
        public UserMicroserviceClient(HttpClient httpClient, ILogger<UserMicroserviceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserDto?> GetUserByEmail(string email)
        {
            _logger.LogInformation("Fetching user with email: {Email}", email);

            var response = await _httpClient.GetAsync($"/api/User/Email/{email}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch user {Email}. Status Code: {StatusCode}", email, response.StatusCode);

                return response.StatusCode switch
                {
                    HttpStatusCode.NotFound => null,
                    HttpStatusCode.BadRequest => throw new HttpRequestException("BadRequest", null, HttpStatusCode.BadRequest),
                    _ => throw new HttpRequestException($"Http Request Failed With Status Code {response.StatusCode}")
                };
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            if (user == null)
            {
                _logger.LogWarning("User not found in API response: {Email}", email);
                return null;
            }

            _logger.LogInformation("User retrieved successfully: {Email}", email);
            return user;
        }
    }
}
