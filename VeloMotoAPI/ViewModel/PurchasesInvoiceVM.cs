using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModel
{
    public class PurchasesInvoiceVM
    {
        public PurchasesInvoiceDTO Invoice { get; set; }
        public List<PurchasesVM> Purchases { get; set; }
    }
}
