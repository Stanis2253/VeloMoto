using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<ProductsDTO>>> GetAll()
        {
            var products = _context.Products;

            if (products == null)
            {
                return NotFound();
            }

            List<ProductsDTO> result = new List<ProductsDTO>();

            foreach (var product in products)
            {
                ProductsDTO productsDTO = new ProductsDTO
                {
                    IdProduct = product.IdProduct,
                    Name = product.Name,
                    Description = product.Description,
                    ShortDesc = product.ShortDesc,
                    CategoryId = product.CategoryId,
                    ManufacturerId = product.ManufacturerId,
                    IsActual = product.IsActual,
                };
                result.Add(productsDTO);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById/{ProductId}")]
        public async Task<ActionResult<ProductsDTO>> Post(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            ProductsDTO productDTO = new ProductsDTO
            {
                IdProduct = product.IdProduct,
                Name = product.Name,
                Description = product.Description,
                ShortDesc = product.ShortDesc,
                CategoryId = product.CategoryId,
                ManufacturerId = product.ManufacturerId,
                IsActual = product.IsActual,
            };
            return Ok(productDTO);
        }
        [HttpPost]
        [Route ("Post")]
        public async Task<ActionResult<ProductsDTO>> Post(ProductsDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest();
            }
            Products productToDb = new Products
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                ShortDesc = productDTO.ShortDesc,
                CategoryId = productDTO.CategoryId,
                ManufacturerId = productDTO.ManufacturerId,
                IsActual = productDTO.IsActual,
            };
            try
            {
                productDTO.IdProduct = _context.Products.OrderByDescending(x => x).FirstOrDefault().IdProduct;
                _context.Products.Add(productToDb);
                await _context.SaveChangesAsync();

                return Ok(productDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteById/{IdProduct}")]
        public async Task<ActionResult> Delete(int ProductId)
        {
            var ProductToDelete = await _context.Products.FindAsync(ProductId);

            try
            {
                _context.Remove(ProductToDelete);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("PutById/{IdProduct}")]
        public async Task<ActionResult> Put (ProductsDTO productsDTO)
        {
            if (productsDTO != null)
            {
                return BadRequest();
            }
            Products product = new Products
            {
                Name = productsDTO.Name,
                Description = productsDTO.Description,
                ShortDesc = productsDTO.ShortDesc,
                IdProduct = productsDTO.IdProduct,
                ManufacturerId = productsDTO.ManufacturerId,
                CategoryId = productsDTO.CategoryId,
                IsActual = productsDTO.IsActual,
            };
            try
            {
                _context.Add(product);
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
