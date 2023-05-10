namespace VeloMotoAPI.Models.DTO
{
    public class ImagesDTO
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
