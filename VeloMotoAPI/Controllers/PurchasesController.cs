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
    public class PurchasessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PurchasessController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<PurchasesVM>>> GetAll()
        {
            var purchasesInvoice = _context.PurchasesInvoice;

            if (purchasesInvoice == null)
            {
                return NotFound();
            }

            List<PurchasesVM> result = new List<PurchasesVM>();

            foreach (var item in purchasesInvoice)
            {
                PurchasesInvoiceDTO invoice = new PurchasesInvoiceDTO
                {
                    DateTime = item.Date,
                };
                var sales = _context.Purchases.Where(p => p.PurchaseInvoiceId == invoice.Id);
                List<PurchasesDTO> salesList = new List<PurchasesDTO>();
                foreach (var sale in sales)
                {
                    PurchasesDTO saleDTO = new PurchasesDTO
                    {
                        Id = sale.Id,
                        ProductId = sale.ProductId,
                        Amount = sale.Amount,
                        PurchaseInvoiceId = sale.Id,
                    };
                    salesList.Add(saleDTO);
                }
                PurchasesVM salesVM = new PurchasesVM
                {
                    Invoice = invoice,
                    Purchases = salesList,
                };
                result.Add(salesVM);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<PurchasesVM>> Post(PurchasesVM purchasesVM)
        {
            if (purchasesVM == null)
            {
                return BadRequest();
            }
            PurchasesInvoice purchasesInvoiceToDB = new PurchasesInvoice
            {
                Date = purchasesVM.Invoice.DateTime,
                ProviderId = purchasesVM.Invoice.ProviderId,
            };
            purchasesVM.Invoice.Id = _context.PurchasesInvoice.OrderByDescending(p=>p).First().Id;
            _context.Add(purchasesInvoiceToDB);
            foreach (var item in purchasesVM.Purchases)
            {
                item.Id = _context.Purchases.OrderByDescending(p => p).First().Id;
                Purchases purchases = new Purchases
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    PurchaseInvoiceId = purchasesVM.Invoice.Id,
                };
                _context.Add(purchases);
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok(purchasesVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteById/{purchasesInvoiceId}")]
        public async Task<ActionResult> Delete (int purchasesInvoiceId)
        {
            var purchasesToDelete = await _context.Purchases.Where(p=>p.PurchaseInvoiceId == purchasesInvoiceId).ToListAsync();
            foreach (var purchase in purchasesToDelete)
            {
                _context.Purchases.Remove(purchase);
            }
            var invoiceToDelete = await _context.PurchasesInvoice.FindAsync(purchasesInvoiceId);
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
