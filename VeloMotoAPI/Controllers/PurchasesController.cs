using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeloMotoAPI.DataAccess;
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

    }
}
