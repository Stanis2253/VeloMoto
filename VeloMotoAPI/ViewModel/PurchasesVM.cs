using VeloMotoAPI.Models;

namespace VeloMotoAPI.ViewModel
{
    public class PurchasesVM
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
