﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMotoAPI.Models
{
    public class Providers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Email { get; set; }
        [Required]

        public string NumberPhone { get; set; }

        public string? Description { get; set; }
    }
}
