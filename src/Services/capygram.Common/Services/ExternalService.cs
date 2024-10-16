using capygram.Common.Shared;
using System.Text.Json;

namespace capygram.Common.Services
{
    public class ExternalService : IExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExternalService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<T>> GetExternalDataListAsync<T>(string endPoint)
        {
            try
            {
                //var response = await _httpClient.GetAsync($"http://{_configuration["IpServer"]}{endPoint}");
                var response = await _httpClient.GetAsync($"http://{endPoint}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<Result<List<T>>>(content, options);
                var a = result.value;
                return a;
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết ở đây thay vì ném Exception chung chung
                throw new Exception($"An error occurred while getting external data: {ex.Message}", ex);
            }
        }

        public async Task<T> GetExternalDataAsync<T>(string endPoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://{_configuration["IpServer"]}{endPoint}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<Result<T>>(content, options);
                var a = result.value;
                return a;
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết ở đây thay vì ném Exception chung chung
                throw new Exception($"An error occurred while getting external data: {ex.Message}", ex);
            }
        }

    }
}