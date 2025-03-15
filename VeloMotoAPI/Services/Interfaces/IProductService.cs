using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Services.Interfaces
{
    public interface IProductService : IBaseService<Products, ProductsDTO>
    {
        Task<IEnumerable<ProductsDTO>> SearchAsync(string searchString);
        Task<IEnumerable<ProductsDTO>> FilterByCategoryAsync(string criteria);
    }
} 