using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using VeloMotoApi.Model;
using VeloMotoAPI.DataAccess.Data;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public JsonResult GetProduct()
        {


            return new JsonResult(objList);
        }
        [HttpPost]
        public JsonResult PostProduct(int id)
        {
            return new JsonResult("Added Successfully");
        }

    }
}
