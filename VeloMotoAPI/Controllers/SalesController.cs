using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.ViewModel;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<SalesVM>>> GetAll()
        {
            var salesInvoice = _context.SalesInvoice;

            if (salesInvoice == null)
            {
                return NotFound();
            }

            List<SalesVM> result = new List<SalesVM>();

            foreach (var item in salesInvoice)
            {
                SalesInvoiceDTO invoice = new SalesInvoiceDTO
                {
                    DateTime = item.Date,
                };
                var sales = _context.Sales.Where(p => p.SalesInvoiceId == invoice.Id);
                List<SalesDTO> salesList = new List<SalesDTO>();
                foreach (var sale in sales)
                {
                    SalesDTO saleDTO = new SalesDTO
                    {
                        Id = sale.Id,
                        ProductId = sale.ProductId,
                        Amount = sale.Amount,
                        SalesInvoiceId = sale.Id,
                    };
                    salesList.Add(saleDTO);
                }
                SalesVM salesVM = new SalesVM
                {
                    Invoice = invoice,
                    Sales = salesList,
                };
                result.Add(salesVM);
            }
            return Ok(result);

        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post(SalesVM salesVM)
        {
            if (salesVM == null)
            {
                return BadRequest();
            }
            SalesInvoice salesInvoiceToDb = new SalesInvoice
            {
                Date = DateTime.Now,
            };
            _context.Add(salesInvoiceToDb);
            foreach (var item in salesVM.Sales)
            {
                item.Id = _context.Sales.OrderByDescending(p => p).First().Id;
                Sales sales = new Sales
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    SalesInvoiceId = _context.SalesInvoice.OrderByDescending(p => p).First().Id,
                };
                _context.Add(sales);
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteById/{salesInvoiceId}")]
        public async Task<ActionResult> Delete(int salesInvoiceId)
        {
            var salesToDelete = await _context.Sales.Where(p=>p.SalesInvoiceId == salesInvoiceId).ToListAsync();
            foreach (var item in salesToDelete)
            {
                _context.Sales.Remove(item);
            }
            var invoiceToDelete = await _context.SalesInvoice.FindAsync(salesInvoiceId);
            try
            {
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
