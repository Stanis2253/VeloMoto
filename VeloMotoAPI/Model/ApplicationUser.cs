using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeloMotoAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
