﻿using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models.DTO
{
    public class CategoriesDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
