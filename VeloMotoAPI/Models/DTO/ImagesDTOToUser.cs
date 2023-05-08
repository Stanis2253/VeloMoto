namespace VeloMotoAPI.Models.DTO
{
    public class ImagesDTOToUser
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public IFormFile Image { get; set; }
    }
}
