using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModels
{
    public class SalesInvoiceVM
    {
        public SalesInvoiceDTO Invoice { get; set; }
        public List<SalesVM> Sales { get; set; }
    }
}
