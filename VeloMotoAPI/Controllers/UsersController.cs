using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        [Route("GetByName/{username}")]
        public async Task<ActionResult<UsersDTO>> GetByName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest();
            }
            UsersDTO result = new UsersDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
            };
            return Ok(result);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post(UsersDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest();
            }
            if (await _context.Users.AnyAsync(p=>p.UserName==userDTO.UserName || p.Email == userDTO.Email || p.PhoneNumber == userDTO.PhoneNumber))
            {
                return BadRequest();
            }
            ApplicationUsers userToDb = new ApplicationUsers
            {
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                NormalizedEmail = userDTO.Email,
                Surname = userDTO.Surname,
                Name = userDTO.Name,
                UserName = userDTO.UserName,
                RolesId = userDTO.RoleId
            };

            IdentityResult result = await _userManager.CreateAsync(userToDb, userDTO.Password);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
//bool IsEmailValid = await (_context.Users.AnyAsync(p=>p.Email == user.Email));
//ApplicationUsers userToDb = new ApplicationUsers
//{
//    Email = user.Email,
//    PasswordHash = user.Password,
//    PhoneNumber = user.PhoneNumber,
//    NormalizedEmail = user.Email,
//    Surname = user.Surname,
//    Name = user.Name,
//    UserName = user.UserName,
//};
//_context.Add(userToDb);
//try
//{
//    await _context.SaveChangesAsync();
//    return Ok();
//}
//catch (Exception ex)
//{
//    return BadRequest(ex.Message);
//}