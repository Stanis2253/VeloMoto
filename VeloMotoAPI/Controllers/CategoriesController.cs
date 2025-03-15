using Microsoft.AspNetCore.Mvc;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<CategoriesDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetById/{CategoryId}")]
        public async Task<ActionResult<CategoriesDTO>> GetById(int CategoryId)
        {
            var category = await _categoryService.GetByIdAsync(CategoryId);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<ActionResult<CategoriesDTO>> GetByName(string name)
        {
            try
            {
                var category = await _categoryService.GetByNameAsync(name);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<CategoriesDTO>> Post(CategoriesDTO categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _categoryService.AddAsync(categoryDto);
                return CreatedAtAction(nameof(GetById), new { CategoryId = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteById/{CategoryId}")]
        public async Task<ActionResult> Delete(int CategoryId)
        {
            try
            {
                await _categoryService.DeleteAsync(CategoryId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<ActionResult> Put(CategoriesDTO categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _categoryService.UpdateAsync(categoryDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

