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
    public class PurchasesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<PurchasesInvoiceVM>>> GetAll()
        {
            var purchasesInvoice = _context.PurchasesInvoice;

            if (purchasesInvoice == null)
            {
                return NotFound();
            }

            List<PurchasesInvoiceVM> result = new List<PurchasesInvoiceVM>();

            foreach (var item in purchasesInvoice)
            {
                PurchasesInvoiceDTO invoice = new PurchasesInvoiceDTO
                {
                    Id = item.Id,
                    DateTime = item.Date,
                };
                var purchases = _context.Purchases.Include(p=>p.Product).Where(p => p.PurchaseInvoiceId == invoice.Id);
                List<PurchasesVM> purchasesList = new List<PurchasesVM>();
                foreach (var purchase in purchases)
                {
                    PurchasesVM purchVM = new PurchasesVM
                    {
                        Id = purchase.Id,
                        ProductId = purchase.ProductId,
                        ProductName = purchase.Product.Name,
                        Amount = purchase.Amount,
                        Price = purchase.Price,
                        PurchaseInvoiceId = purchase.Id,
                    };
                    purchasesList.Add(purchVM);
                }
                PurchasesInvoiceVM purchaseVM = new PurchasesInvoiceVM
                {
                    Invoice = invoice,
                    Purchases = purchasesList,
                };
                result.Add(purchaseVM);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<PurchasesInvoiceVM>> Post(PurchasesInvoiceVM purchasesVM)
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
                    Price = item.Price,
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
