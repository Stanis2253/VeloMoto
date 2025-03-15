using AutoMapper;
using VeloMotoAPI.DataAccess.Interfaces;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Services
{
    public class ManufacturerService : CacheableBaseService<Manufacturers, ManufacturersDTO>, IManufacturerService
    {
        private const string CachePrefix = "manufacturer";

        public ManufacturerService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICacheService cacheService)
            : base(unitOfWork, mapper, unitOfWork.Manufacturers, cacheService, CachePrefix)
        {
        }

        public async Task<ManufacturersDTO> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");

            var cacheKey = $"{_cachePrefix}:name:{name.ToLower()}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                async () =>
                {
                    var manufacturer = (await _repository.FindAsync(m => m.Name.ToLower() == name.ToLower())).FirstOrDefault();
                    return _mapper.Map<ManufacturersDTO>(manufacturer);
                },
                _defaultExpiration);
        }

        public async Task<IEnumerable<ManufacturersDTO>> GetByPhoneNumberAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty");

            var cacheKey = $"{_cachePrefix}:phone:{phoneNumber}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                async () =>
                {
                    var manufacturers = await _repository.FindAsync(m => m.NumberPhone == phoneNumber);
                    return _mapper.Map<IEnumerable<ManufacturersDTO>>(manufacturers);
                },
                _defaultExpiration);
        }

        public async Task<IEnumerable<ManufacturersDTO>> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty");

            var cacheKey = $"{_cachePrefix}:email:{email.ToLower()}";
            return await _cacheService.GetOrCreateAsync(cacheKey,
                async () =>
                {
                    var manufacturers = await _repository.FindAsync(m => m.Email == email);
                    return _mapper.Map<IEnumerable<ManufacturersDTO>>(manufacturers);
                },
                _defaultExpiration);
        }

        public override async Task<ManufacturersDTO> AddAsync(ManufacturersDTO dto)
        {
            // Проверяем уникальность имени
            var existingManufacturer = await GetByNameAsync(dto.Name);
            if (existingManufacturer != null)
                throw new ArgumentException($"Manufacturer with name {dto.Name} already exists");

            // Проверяем уникальность номера телефона
            var manufacturersWithPhone = await GetByPhoneNumberAsync(dto.NumberPhone);
            if (manufacturersWithPhone.Any())
                throw new ArgumentException($"Manufacturer with phone number {dto.NumberPhone} already exists");

            // Проверяем уникальность email, если он указан
            if (!string.IsNullOrEmpty(dto.Email))
            {
                var manufacturersWithEmail = await GetByEmailAsync(dto.Email);
                if (manufacturersWithEmail.Any())
                    throw new ArgumentException($"Manufacturer with email {dto.Email} already exists");
            }

            return await base.AddAsync(dto);
        }

        public override async Task UpdateAsync(ManufacturersDTO dto)
        {
            // Проверяем уникальность имени
            var existingManufacturer = (await _repository.FindAsync(m => 
                m.Name.ToLower() == dto.Name.ToLower() && m.Id != dto.Id)).FirstOrDefault();
            if (existingManufacturer != null)
                throw new ArgumentException($"Manufacturer with name {dto.Name} already exists");

            // Проверяем уникальность номера телефона
            var manufacturersWithPhone = (await _repository.FindAsync(m => 
                m.NumberPhone == dto.NumberPhone && m.Id != dto.Id)).FirstOrDefault();
            if (manufacturersWithPhone != null)
                throw new ArgumentException($"Manufacturer with phone number {dto.NumberPhone} already exists");

            // Проверяем уникальность email, если он указан
            if (!string.IsNullOrEmpty(dto.Email))
            {
                var manufacturersWithEmail = (await _repository.FindAsync(m => 
                    m.Email == dto.Email && m.Id != dto.Id)).FirstOrDefault();
                if (manufacturersWithEmail != null)
                    throw new ArgumentException($"Manufacturer with email {dto.Email} already exists");
            }

            await base.UpdateAsync(dto);
        }

        protected override async Task InvalidateCacheAsync()
        {
            await base.InvalidateCacheAsync();
            // Дополнительно инвалидируем кэш для поиска по email и телефону
            await _cacheService.RemoveByPrefixAsync($"{_cachePrefix}:phone:");
            await _cacheService.RemoveByPrefixAsync($"{_cachePrefix}:email:");
        }
    }
} 