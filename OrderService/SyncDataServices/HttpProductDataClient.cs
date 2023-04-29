using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using OrderService.Dtos;

namespace OrderService.SyncDataServices
{
    public class HttpProductDataClient : IProductDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpProductDataClient(HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IEnumerable<ProductReadDto>> GetProducts()
        {
            var response = await _httpClient.GetAsync(_configuration["ProductService"]);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var products = JsonSerializer.Deserialize<IEnumerable<ProductReadDto>>(content, options);
                if (products is not null)
                {
                    return products;
                }
                throw new Exception("Cannot deserialize product data");
            }
            else
            {
                throw new Exception("Cannot retrieve product data");
            }
        }
    }
}