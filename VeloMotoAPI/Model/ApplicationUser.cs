using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeloMotoAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string? Patronymic { get; set; }

        [NotMapped]
        private string fullName;
        [NotMapped]
        public string FullName
        {
            get { return fullName = (FirstName +" " + LastName + " " + Patronymic); }
            set { fullName = value; }
        }


    }
}
