using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModel
{
    public class SalesInvoiceVM
    {
        public SalesInvoiceDTO Invoice { get; set; }
        public List<SalesVM> Sales { get; set; }
    }
}
