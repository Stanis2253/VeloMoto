using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModel
{
    public class PurchasesVM
    {
        public PurchasesInvoiceDTO Invoice { get; set; }
        public List<PurchasesDTO> Purchases { get; set; }
    }
}
