using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeloMotoAPI.ViewModels
{
    public class SalesVM
    {
        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
