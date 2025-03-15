using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("GetById/{ProductId}")]
        public async Task<ActionResult<ProductsDTO>> GetById(int ProductId)
        {
            var product = await _productService.GetByIdAsync(ProductId);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        [Route("GetSearch")]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> Search(string searchString)
        {
            var products = await _productService.SearchAsync(searchString);
            return Ok(products);
        }

        [HttpGet]
        [Route("FilterByCategory")]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> FilterByCategory(string criteria)
        {
            try
            {
                var products = await _productService.FilterByCategoryAsync(criteria);
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<ProductsDTO>> Post(ProductsDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _productService.AddAsync(productDto);
                return CreatedAtAction(nameof(GetById), new { ProductId = result.IdProduct }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteById/{ProductId}")]
        public async Task<ActionResult> Delete(int ProductId)
        {
            try
            {
                await _productService.DeleteAsync(ProductId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<ActionResult> Put(ProductsDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _productService.UpdateAsync(productDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
