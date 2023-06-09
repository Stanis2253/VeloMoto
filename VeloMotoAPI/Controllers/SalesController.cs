using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.ViewModels;

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
        public async Task<ActionResult<List<SalesInvoiceVM>>> GetAll()
        {
            var salesInvoice = await _context.SalesInvoice.ToListAsync();
            if (salesInvoice == null)
            {
                return NotFound();
            }
            List<SalesInvoiceVM> result = new List<SalesInvoiceVM>();
            foreach (var item in salesInvoice)
            {
                SalesInvoiceDTO invoice = new SalesInvoiceDTO
                {
                    DateTime = item.Date,
                    Id = item.Id,
                    StatusId = item.StatusId,
                };
                var sales = await _context.Sales.Include(p=>p.Product).Where(p => p.SalesInvoiceId == invoice.Id).ToListAsync();
                List<SalesVM> salesList = new List<SalesVM>();
                foreach (var sale in sales)
                {
                    var prices = _context.Prices.OrderByDescending(p => p).FirstOrDefault(p => p.ProductId == sale.ProductId);
                    SalesVM saleVM = new SalesVM
                    {
                        Id = sale.Id,
                        ProductId = sale.ProductId,
                        Amount = sale.Amount,
                        SalesInvoiceId = sale.Id,
                        ProductName = sale.Product.Name,
                        Price = sale.Price,
                    };
                    salesList.Add(saleVM);
                }
                SalesInvoiceVM salesVM = new SalesInvoiceVM
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
        public async Task<ActionResult> Post(SalesInvoiceVM salesVM)
        {
            if (salesVM == null)
            {
                return BadRequest();
            }
            SalesInvoice salesInvoiceToDb = new SalesInvoice
            {
                Date = salesVM.Invoice.DateTime,
                StatusId = 1,

            };
            _context.Add(salesInvoiceToDb);
            List<Sales> salesListToDb = new List<Sales>();
            foreach (var item in salesVM.Sales)
            {
                //item.Id = _context.Sales.OrderByDescending(p => p).First().Id;
                Sales sales = new Sales
                {

                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    SalesInvoiceId = 1,
                    Price = item.Price,
                    
                    
                };
                salesListToDb.Add(sales);
            }
            _context.Sales.AddRange(salesListToDb);
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

            _context.RemoveRange(salesToDelete);

            var invoiceToDelete = await _context.SalesInvoice.FindAsync(salesInvoiceId);

            _context.Remove(invoiceToDelete);

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
