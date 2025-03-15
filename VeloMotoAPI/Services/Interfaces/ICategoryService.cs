using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Services.Interfaces
{
    public interface ICategoryService : IBaseService<Categories, CategoriesDTO>
    {
        Task<CategoriesDTO> GetByNameAsync(string name);
    }
} 