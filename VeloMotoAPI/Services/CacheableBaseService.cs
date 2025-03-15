namespace VeloMotoAPI.Services
{
    public abstract class CacheableBaseService<T, TDto> : BaseService<T, TDto> where T : class where TDto : class
    {
        protected readonly ICacheService _cacheService;
        protected readonly string _cachePrefix;
        protected readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(10);

        protected CacheableBaseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IGenericRepository<T> repository,
            ICacheService cacheService,
            string cachePrefix) : base(unitOfWork, mapper, repository)
        {
            _cacheService = cacheService;
            _cachePrefix = cachePrefix;
        }

        public override async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var cacheKey = $"{_cachePrefix}:all";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                () => base.GetAllAsync(),
                _defaultExpiration);
        }

        public override async Task<TDto> GetByIdAsync(int id)
        {
            var cacheKey = $"{_cachePrefix}:id:{id}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                () => base.GetByIdAsync(id),
                _defaultExpiration);
        }

        public override async Task<IEnumerable<TDto>> FindAsync(Expression<Func<T, bool>> expression)
        {
            // Note: We don't cache Find results as they can be arbitrary
            return await base.FindAsync(expression);
        }

        public override async Task<TDto> AddAsync(TDto dto)
        {
            var result = await base.AddAsync(dto);
            await InvalidateCacheAsync();
            return result;
        }

        public override async Task UpdateAsync(TDto dto)
        {
            await base.UpdateAsync(dto);
            await InvalidateCacheAsync();
        }

        public override async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
            await InvalidateCacheAsync();
        }

        protected virtual async Task InvalidateCacheAsync()
        {
            await _cacheService.RemoveByPrefixAsync(_cachePrefix);
        }
    }
} 