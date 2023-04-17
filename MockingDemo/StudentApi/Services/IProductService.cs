using StudentApi.Models;

namespace StudentApi.Services
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(int id);
    }
}