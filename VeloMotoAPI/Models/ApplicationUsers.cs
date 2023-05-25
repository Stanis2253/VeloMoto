using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeloMotoAPI.Models
{
    public class ApplicationUsers : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string RolesId { get; set; }

        [ForeignKey("RolesId")]
        public virtual ApplicationRoles Roles { get; set; }
    }
}
