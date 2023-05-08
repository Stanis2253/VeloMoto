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

        [Route("GetById/{Id}")]
        [HttpGet]
        public async Task<ActionResult<CategoriesDTO>> GetById(int Id)
        {
            var category = _context.Categories.Find(Id);

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

        [Route ("DeleteById/{Id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var CategoryToDelete = _context.Categories.Find(Id);

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
        public async Task<ActionResult> Put(CategoriesDTO category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            Categories categoryPut = new Categories
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
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

