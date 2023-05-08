

namespace VeloMotoAPI.Models.DTO
{
    public class PurchasesDTO
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
