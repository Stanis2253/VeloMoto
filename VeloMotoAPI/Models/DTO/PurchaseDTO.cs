

namespace VeloMotoAPI.Models.DTO
{
    public class PurchaseDTO
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PruchaseInvoiceId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
