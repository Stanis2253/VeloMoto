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
                    Price = _context.Prices.FirstOrDefault(p=>p.ProductId==p.ProductId).Value,
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
        [HttpGet]
        [Route("GetSearch")]
        public async Task<ActionResult> Search(string searchString)
        {
            var products = await _context.Products.Where(search => search.Name.StartsWith(searchString.ToLower())).ToListAsync();
            if (products == null)
                return NotFound();

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
                    Price = _context.Prices.FirstOrDefault(p => p.ProductId == p.ProductId).Value,
                };
                result.Add(productsDTO);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("FilterByCategory")]
        public async Task<ActionResult> FilterByCategory(string criteria)
        {
            if (criteria == null || criteria == "" || criteria.Length == 0)
                return BadRequest();

            string value = criteria.ToLower();

            List<ProductsDTO> result = new List<ProductsDTO>();


            if (value == "categories")
            {
                var productsFromDb = _context.Products.OrderBy(p => p.Category);

                foreach (var product in productsFromDb)
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
                        Price = _context.Prices.FirstOrDefault(p => p.ProductId == p.ProductId).Value,
                    };
                    result.Add(productsDTO);
                }
            }
            if (value == "manufacturers")
            {
                var productsFromDb = _context.Products.OrderBy(p => p.Manufacturer);

                foreach (var product in productsFromDb)
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
                        Price = _context.Prices.FirstOrDefault(p => p.ProductId == p.ProductId).Value,
                    };
                    result.Add(productsDTO);
                }
            }
            return Ok(result);
        }

        [HttpPost]
        [Route ("Post")]
        public async Task<ActionResult<ProductsDTO>> Post(ProductsDTO obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            Products productToDb = new Products
            {
                Name = obj.Name,
                Description = obj.Description,
                ShortDesc = obj.ShortDesc,
                CategoryId = obj.CategoryId,
                ManufacturerId = obj.ManufacturerId,
                IsActual = obj.IsActual,
            };
            try
            {
                obj.IdProduct = _context.Products.OrderByDescending(x => x).FirstOrDefault().IdProduct;
                _context.Products.Add(productToDb);
                await _context.SaveChangesAsync();

                return Ok(obj);
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
        [Route("Put")]
        public async Task<ActionResult> Put (ProductsDTO obj)
        {
            if (obj != null)
            {
                return BadRequest();
            }
            Products product = new Products
            {
                Name = obj.Name,
                Description = obj.Description,
                ShortDesc = obj.ShortDesc,
                IdProduct = obj.IdProduct,
                ManufacturerId = obj.ManufacturerId,
                CategoryId = obj.CategoryId,
                IsActual = obj.IsActual,
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
