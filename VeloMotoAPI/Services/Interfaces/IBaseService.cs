using System.Linq.Expressions;

namespace VeloMotoAPI.Services.Interfaces
{
    public interface IBaseService<T, TDto> where T : class where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> FindAsync(Expression<Func<T, bool>> expression);
        Task<TDto> AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }
} 