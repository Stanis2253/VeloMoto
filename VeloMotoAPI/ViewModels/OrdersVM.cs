using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.ViewModels
{
    public class OrdersVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public SalesInvoiceVM Invoice { get; set; }
    }
}
