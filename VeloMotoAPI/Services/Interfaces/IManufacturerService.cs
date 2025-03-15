using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Services.Interfaces
{
    public interface IManufacturerService : IBaseService<Manufacturers, ManufacturersDTO>
    {
        Task<ManufacturersDTO> GetByNameAsync(string name);
        Task<IEnumerable<ManufacturersDTO>> GetByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<ManufacturersDTO>> GetByEmailAsync(string email);
    }
} 