using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess.Interfaces;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Services
{
    public class ProductService : CacheableBaseService<Products, ProductsDTO>, IProductService
    {
        private const string CachePrefix = "product";

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
            : base(unitOfWork, mapper, unitOfWork.Products, cacheService, CachePrefix)
        {
        }

        public async Task<IEnumerable<ProductsDTO>> SearchAsync(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return await GetAllAsync();

            var cacheKey = $"{_cachePrefix}:search:{searchString.ToLower()}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                async () =>
                {
                    var products = await _repository.FindAsync(p => p.Name.ToLower().StartsWith(searchString.ToLower()));
                    return _mapper.Map<IEnumerable<ProductsDTO>>(products);
                },
                _defaultExpiration);
        }

        public async Task<IEnumerable<ProductsDTO>> FilterByCategoryAsync(string criteria)
        {
            if (string.IsNullOrEmpty(criteria))
                throw new ArgumentException("Criteria cannot be empty");

            var cacheKey = $"{_cachePrefix}:filter:{criteria.ToLower()}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                async () =>
                {
                    var products = criteria.ToLower() switch
                    {
                        "categories" => await _repository.FindAsync(p => true),
                        "manufacturers" => await _repository.FindAsync(p => true),
                        _ => throw new ArgumentException("Invalid filter criteria")
                    };

                    return _mapper.Map<IEnumerable<ProductsDTO>>(products);
                },
                _defaultExpiration);
        }

        public override async Task<ProductsDTO> AddAsync(ProductsDTO dto)
        {
            var product = await base.AddAsync(dto);

            // Добавляем цену продукта
            if (dto.Price > 0)
            {
                var price = new Prices
                {
                    ProductId = product.IdProduct,
                    Value = dto.Price,
                    DateTime = DateTime.Now
                };
                await _unitOfWork.Prices.AddAsync(price);
                await _unitOfWork.SaveChangesAsync();
            }

            return product;
        }

        protected override async Task InvalidateCacheAsync()
        {
            await base.InvalidateCacheAsync();
            // Дополнительно инвалидируем кэш для поиска и фильтрации
            await _cacheService.RemoveByPrefixAsync($"{_cachePrefix}:search:");
            await _cacheService.RemoveByPrefixAsync($"{_cachePrefix}:filter:");
        }
    }
} 