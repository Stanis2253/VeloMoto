using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.Utilities;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        private static IWebHostEnvironment _environment;

        public ImageController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _environment = webHost;
        }
        [HttpGet]
        [Route("GetByIdProduct/{ProductId}")]
        public async Task<ActionResult<List<ImagesDTOToUser>>> GetByIdProduct(int ProductId)
        {
            var images = await _context.Images.Where(p=>p.ProductId == ProductId).ToListAsync();

            List<ImagesDTOToUser> result = new List<ImagesDTOToUser>();

            foreach (var item in images)
            {
                ImagesDTOToUser imageDTO = new ImagesDTOToUser
                {
                    ProductId = item.ProductId,
                };

                string pathImage = _environment.WebRootPath + WC.PathProductImage + item.IndexImg + ".png";
                //C:\Users\stani\Work\VeloMoto\VeloMotoAPI\wwwroot\images\product\327804f6-6507-4b81-814b-b72a489af85e.png
                using (FileStream fs = new FileStream(pathImage, FileMode.Open))
                {
                    imageDTO.Image = new FormFile(fs,0, fs.Length,"img", "img");
                }
                result.Add(imageDTO);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post([FromForm]ImagesDTOUploud imagesDTOs)
        {
            if (imagesDTOs == null)
            {
                return BadRequest();
            }
            for (int i = 0; i != (imagesDTOs.Images.Count); i++)
            {
                string path = _environment.WebRootPath + WC.PathProductImage;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(imagesDTOs.Images[i].FileName);
                using (var fileStream = new FileStream(Path.Combine(path, fileName + extension), FileMode.Create))
                {
                    imagesDTOs.Images[i].CopyTo(fileStream);
                }
                Images image = new Images
                {
                    IndexImg = fileName,
                    ProductId = imagesDTOs.ProductId,
                };
                _context.Add(image);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
