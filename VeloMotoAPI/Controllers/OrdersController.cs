using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VeloMotoAPI.DataAccess;
using VeloMotoAPI.Models;
using VeloMotoAPI.Models.DTO;
using VeloMotoAPI.ViewModels;

namespace VeloMotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<OrdersVM>>> Get() 
        {
            var orders = await _context.Orders.ToListAsync();

            List<OrdersVM> result = new List<OrdersVM>();

            foreach (var order in orders)
            {
                
                foreach (var item in collection)
                {
                    var sales = await _context.Sales.Where(p => p.SalesInvoice.Id == order.SalesInvoiceId).Include(p => p.Product).ToListAsync();
                    List<SalesVM> salesList = new List<SalesVM>();
                    foreach (var sale in sales)
                    {
                        SalesVM salesVM = new SalesVM
                        {
                            Id = order.Id,
                            SalesInvoiceId = order.SalesInvoiceId,
                            Amount = sale.Amount,
                            Price = sale.Price,
                            ProductId = sale.ProductId,
                            ProductName = sale.Product.Name,
                        };
                        salesList.Add(salesVM);
                    }

                    OrdersVM ordersVM = new OrdersVM
                    {
                        Id = order.Id,
                        Email = order.User.Email,
                        Invoice = salesList
                    };
                }
            }
        }
    }
}
