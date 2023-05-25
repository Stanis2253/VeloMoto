using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeloMotoAPI.Models;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationRoles> _roleManager;
        public RolesController(RoleManager<ApplicationRoles> roleManager) 
        { 
            _roleManager = roleManager;
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRoles(name));
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
