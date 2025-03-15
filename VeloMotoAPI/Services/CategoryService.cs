using AutoMapper;
using VeloMotoAPI.DataAccess.Interfaces;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Services
{
    public class CategoryService : CacheableBaseService<Categories, CategoriesDTO>, ICategoryService
    {
        private const string CachePrefix = "category";

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
            : base(unitOfWork, mapper, unitOfWork.Categories, cacheService, CachePrefix)
        {
        }

        public async Task<CategoriesDTO> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");

            var cacheKey = $"{_cachePrefix}:name:{name.ToLower()}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                async () =>
                {
                    var category = (await _repository.FindAsync(c => c.Name.ToLower() == name.ToLower())).FirstOrDefault();
                    return _mapper.Map<CategoriesDTO>(category);
                },
                _defaultExpiration);
        }

        public override async Task<CategoriesDTO> AddAsync(CategoriesDTO dto)
        {
            // Проверяем, существует ли категория с таким именем
            var existingCategory = await GetByNameAsync(dto.Name);
            if (existingCategory != null)
                throw new ArgumentException($"Category with name {dto.Name} already exists");

            return await base.AddAsync(dto);
        }

        public override async Task UpdateAsync(CategoriesDTO dto)
        {
            // Проверяем, существует ли категория с таким именем, исключая текущую категорию
            var existingCategory = (await _repository.FindAsync(c => 
                c.Name.ToLower() == dto.Name.ToLower() && c.Id != dto.Id)).FirstOrDefault();
                
            if (existingCategory != null)
                throw new ArgumentException($"Category with name {dto.Name} already exists");

            await base.UpdateAsync(dto);
        }
    }
} 