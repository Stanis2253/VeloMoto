﻿namespace VeloMotoAPI.Models.DTO
{
    public class ImagesDTOUploud
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
