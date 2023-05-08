using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModel
{
    public class SalesVM
    {
        public SalesInvoiceDTO Invoice { get; set; }
        public List<SalesDTO> Sales { get; set; }
    }
}
