using Microsoft.AspNetCore.Identity;

namespace VeloMotoAPI.Models
{
    public class ApplicationRoles : IdentityRole
    {
        public ApplicationRoles() : base() { }

        public ApplicationRoles(string name)
            : base(name)
        { }
    }
}
