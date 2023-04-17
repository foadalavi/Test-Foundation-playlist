using Microsoft.Extensions.Options;
using StudentApi.Models;
using StudentApi.Models.Config;

namespace StudentApi.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private readonly IOptions<ProductOptions> _config;

        public ProductService(HttpClient client, IOptions<ProductOptions> config)
        {
            _client = client;
            _config = config;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var response = await _client.GetAsync(string.Concat(_config.Value.EndPoint, id));
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new Product();
            }

            var responseContent = response.Content;
            var product = await responseContent.ReadFromJsonAsync<Product>();
            return product;
        }
    }
}
