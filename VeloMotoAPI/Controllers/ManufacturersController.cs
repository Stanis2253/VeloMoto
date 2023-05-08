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
    public class ManufacturersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ManufacturersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<ManufacturersDTO>>> GetAll()
        {
            var manufacturers = _context.Manufacturers; 

            if (manufacturers == null)
            {
                return NotFound();
            }

            List<ManufacturersDTO> result = new List<ManufacturersDTO>();

            foreach (var item in manufacturers)
            {
                ManufacturersDTO manufacturersDTO = new ManufacturersDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                };
                result.Add(manufacturersDTO);
            }

            if (result.Count <= 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("GetById/{ManufacturerId}")]
        public async Task<ActionResult<ManufacturersDTO>> GetById(int ManufacturerId)
        {
            if (ManufacturerId == null)
            {
                return BadRequest();
            }

            var manufacturer = await _context.Manufacturers.FindAsync(ManufacturerId);

            if (manufacturer == null)
            {
                return NotFound();
            }

            ManufacturersDTO manufacturerDTO = new ManufacturersDTO 
            {
                Id = ManufacturerId,
                Name = manufacturer.Name,
                Description = manufacturer.Description,
                Email = manufacturer.Email,
                NumberPhone = manufacturer.NumberPhone,
            };
            return Ok(manufacturerDTO);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<ManufacturersDTO>> Post (ManufacturersDTO obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            Manufacturers manufacturer = new Manufacturers
            {
                Name = obj.Name,
                NumberPhone = obj.NumberPhone,
                Description = obj.Description,
                Email = obj.Email,
            };

            try
            {
                _context.Manufacturers.Add(manufacturer);
                await _context.SaveChangesAsync();
                obj.Id = _context.Manufacturers.OrderByDescending(p=>p).First().Id;
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteById/{ManufacturerId}")]
        public async Task<ActionResult> DeleteById(int ManufacturerId)
        {
            if (ManufacturerId == null)
            {
                return BadRequest();
            }
            try
            {
                Manufacturers manufacturerToDelete = await _context.Manufacturers.FindAsync(ManufacturerId);
                _context.Remove(manufacturerToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut]
        [Route("Put")]
        public async Task<ActionResult<ManufacturersDTO>> Put (ManufacturersDTO obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            Manufacturers manufacturerPut = new Manufacturers
            {
                Name = obj.Name,
                NumberPhone = obj.NumberPhone,
                Description = obj.Description,
                Email = obj.Email
            };

            try
            {
                _context.Update(manufacturerPut);
                await _context.SaveChangesAsync();
                obj.Id = _context.Manufacturers.OrderByDescending(p=>p).First().Id;
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
