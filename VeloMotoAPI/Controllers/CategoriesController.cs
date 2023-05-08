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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Route("GetAll")]
        [HttpGet]
        public async Task <ActionResult<List<CategoriesDTO>>> GetAll()
        {
            var categories = _context.Categories;

            if (categories == null)
            {
                return NotFound();
            }

            List<CategoriesDTO> result = new List<CategoriesDTO>();

            foreach (var item in categories)
            {
                CategoriesDTO categoryDTO = new CategoriesDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                };
                result.Add(categoryDTO);
            }

            if (result.Count <= 0)
            {
                return BadRequest();
            }

            return result;
        }

        [Route("GetById/{CategoryId}")]
        [HttpGet]
        public async Task<ActionResult<CategoriesDTO>> GetById(int CategoryId)
        {
            var category = _context.Categories.Find(CategoryId);

            if (category == null)
            {
                return NotFound();
            }


            CategoriesDTO result = new CategoriesDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            if (result == null)
            {
                return BadRequest();
            }

            return result;
        }

        [Route ("Post")]
        [HttpPost]
        public async Task<ActionResult> Post(CategoriesDTO obj)
        {

            if (obj.Name == null)
            {
                return BadRequest();
            }

            Categories categoryToDb = new Categories
            {
                Name = obj.Name,
                Description = obj.Description,
            };
            if (categoryToDb == null)
            {
                return BadRequest(obj);
            }
            try
            {
                _context.Add(categoryToDb);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route ("DeleteById/{CategoryId}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int CategoryId)
        {
            if (CategoryId == null)
            {
                return BadRequest();
            }

            var CategoryToDelete = _context.Categories.Find(CategoryId);

            if (CategoryToDelete == null)
            {
                return NotFound();
            }


            try
            {
                _context.Remove(CategoryToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Put")]
        [HttpPut]
        public async Task<ActionResult> Put(CategoriesDTO obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            Categories categoryPut = new Categories
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
            };

            try
            {
                _context.Update(categoryPut);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

