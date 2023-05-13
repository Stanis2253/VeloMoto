using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModels
{
    public class PurchasesInvoiceVM
    {
        public PurchasesInvoiceDTO Invoice { get; set; }
        public List<PurchasesVM> Purchases { get; set; }
    }
}
