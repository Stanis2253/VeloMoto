using AutoMapper;
using System.Linq.Expressions;
using VeloMotoAPI.DataAccess.Interfaces;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Services
{
    public abstract class BaseService<T, TDto> : IBaseService<T, TDto> where T : class where TDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IGenericRepository<T> _repository;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<IEnumerable<TDto>> FindAsync(Expression<Func<T, bool>> expression)
        {
            var entities = await _repository.FindAsync(expression);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
} 