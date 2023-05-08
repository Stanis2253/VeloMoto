using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProviderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<ProvidersDTO>>> GetAll()
        {
            var providers = await _context.Providers.ToListAsync();

            if (providers == null)
            {
                return NotFound();
            }

            List<ProvidersDTO> result = new List<ProvidersDTO>();
            foreach (var provider in providers)
            {
                ProvidersDTO providerDTO = new ProvidersDTO
                {
                    Id = provider.Id,
                    Name = provider.Name,
                    Description = provider.Description,
                    Email = provider.Email,
                    NumberPhone = provider.NumberPhone,
                };
                result.Add(providerDTO);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById/{ProviderId}")]
        public async Task<ActionResult<ProvidersDTO>> Get(int ProviderId)
        {
            var provider = await _context.Providers.FindAsync(ProviderId);
            if (provider == null)
            {
                return NotFound();
            }
            ProvidersDTO providerDTO = new ProvidersDTO
            {
                Id = provider.Id,
                Name = provider.Name,
                Description = provider.Description,
                Email = provider.Email,
                NumberPhone = provider.NumberPhone,
            };
            return Ok(providerDTO);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post(ProvidersDTO providerDTO)
        {
            if (providerDTO == null)
            {
                return BadRequest();
            }
            Providers providerToDb = new Providers
            {
                Name = providerDTO.Name,
                Description = providerDTO.Description,
                Email = providerDTO.Email,
                NumberPhone = providerDTO.NumberPhone,
            };
            try
            {
                providerDTO.Id = _context.Providers.OrderByDescending(p=>p).First().Id;
                _context.Add(providerToDb);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteById/{ProviderId}")]
        public async Task<ActionResult> Delete(int ProviderId)
        {
            var providerToDelete = await _context.Providers.FindAsync(ProviderId);
            if (providerToDelete == null)
            {
                return NotFound();
            }
            try
            {
                _context.Remove(providerToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("Put")]
        public async Task<ActionResult> Put(ProvidersDTO providerDTO)
        {
            if (providerDTO == null)
            {
                return BadRequest();
            }
            Providers provider = new Providers
            {
                Id = providerDTO.Id,
                Name = providerDTO.Name,
                Description = providerDTO.Description,
                Email = providerDTO.Email,
                NumberPhone = providerDTO.NumberPhone,
            };
            try
            {
                _context.Update(provider);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
