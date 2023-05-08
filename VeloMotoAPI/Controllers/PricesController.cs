using Microsoft.AspNetCore.Mvc;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PricesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/GetAllByIdProduct/{ProductId}")]
        public async Task< ActionResult<List<PricesDTO>>> GetAll(int ProductId) 
        {


            var prices = _context.Prices.Where(p=>p.ProductId == ProductId);

            if (prices == null)
            {
                return NotFound();
            }
            
            List<PricesDTO> result = new List<PricesDTO>();

            foreach (var pr in prices)
            {
                PricesDTO pricesDTO = new PricesDTO
                {
                    Id = pr.Id,
                    ProductId = pr.ProductId,
                    Value = pr.Value,
                    DateTime = pr.DateTime,
                };
                result.Add(pricesDTO);
            }

            return Ok(result);

        }
        [HttpGet]
        [Route("/GetActualByIdProduct/{ProductId}")]
        public async Task<ActionResult<PricesDTO>> GetActual(int ProductId)
        {
            var price = _context.Prices.OrderByDescending(p => p.ProductId).FirstOrDefault();

            if (price == null)
            {
                return NotFound();
            }

            PricesDTO result = new PricesDTO
            {
                Id = price.Id,
                ProductId = price.ProductId,
                Value = price.Value,
                DateTime = price.DateTime,
            };

            return Ok(result);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<PricesDTO>> Post(PricesDTO pricesDTO)
        {
            if (pricesDTO == null)
            {
                return BadRequest();
            }

            Prices priceToDB = new Prices
            {
                ProductId = pricesDTO.ProductId,
                Value = pricesDTO.Value,
            };
            try
            {
                _context.Add(priceToDB);
                pricesDTO.Id = (_context.Prices.OrderByDescending(p => p.ProductId).FirstOrDefault().Id + 1);
                pricesDTO.DateTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok(pricesDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
