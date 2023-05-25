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
        [Route("GetAll")]
        public async Task<ActionResult<List<OrdersVM>>> GetAll()
        {
            var orders = await _context.Orders.Include(p => p.User).Include(p => p.SalesInvoice).ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            List<OrdersVM> result = new List<OrdersVM>();

            foreach (var order in orders)
            {
                OrdersVM ordersVM = new OrdersVM
                {
                    Id = order.Id,
                    Email = order.User.Email,
                    PhoneNumber = order.User.PhoneNumber,
                };
                SalesInvoiceDTO salesInvoiceDTO = new SalesInvoiceDTO
                {
                    Id = order.SalesInvoiceId,
                    DateTime = order.SalesInvoice.Date,
                    StatusId = order.SalesInvoice.StatusId
                };

                var sales = await _context.Sales.Where(p => p.SalesInvoiceId == order.SalesInvoiceId).Include(p => p.Product).ToListAsync();

                List<SalesVM> salesVMList = new List<SalesVM>();

                foreach (var sale in sales)
                {
                    SalesVM salesVM = new SalesVM
                    {
                        Id = sale.Id,
                        SalesInvoiceId = sale.SalesInvoiceId,
                        ProductId = sale.ProductId,
                        ProductName = sale.Product.Name,
                        Amount = sale.Amount,
                        Price = sale.Price,
                    };
                    salesVMList.Add(salesVM);
                }
                SalesInvoiceVM salesInvoiceVM = new SalesInvoiceVM
                {
                    Invoice = salesInvoiceDTO,
                    Sales = salesVMList
                };
                ordersVM.Invoice = salesInvoiceVM;
                result.Add(ordersVM);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById/{Id}")]
        public async Task<ActionResult<OrdersVM>> GetById (int Id)
        {
            var order = await _context.Orders.FindAsync(Id);
            OrdersVM ordersVM = new OrdersVM
            {
                Id = Id,
                Email = order.User.Email,
                PhoneNumber = order.User.PhoneNumber,
            };
            SalesInvoiceDTO salesInvoiceDTO = new SalesInvoiceDTO
            {
                Id = order.SalesInvoiceId,
                DateTime = order.SalesInvoice.Date,
                StatusId = order.SalesInvoice.StatusId,

            };

            var sales = await _context.Sales.Where(p => p.SalesInvoiceId == order.SalesInvoiceId).Include(p => p.Product).ToListAsync();

            List<SalesVM> salesVMList = new List<SalesVM>();

            foreach (var sale in sales)
            {
                SalesVM salesVM = new SalesVM
                {
                    Id = sale.Id,
                    SalesInvoiceId = sale.SalesInvoiceId,
                    ProductId = sale.ProductId,
                    ProductName = sale.Product.Name,
                    Amount = sale.Amount,
                    Price = sale.Price,
                };
                salesVMList.Add(salesVM);
            }
            SalesInvoiceVM salesInvoiceVM = new SalesInvoiceVM
            {
                Invoice = salesInvoiceDTO,
                Sales = salesVMList
            };
            ordersVM.Invoice = salesInvoiceVM;

            return Ok(ordersVM);
        }
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post (OrdersVM ordersVM)
        {
            if (ordersVM == null)
            {
                return BadRequest();
            }
            SalesInvoice salesInvoice = new SalesInvoice
            {
                Date = ordersVM.Invoice.Invoice.DateTime,
                StatusId = ordersVM.Invoice.Invoice.StatusId,
            };
            
            foreach (var item in ordersVM.Invoice.Sales)
            {
                Sales sale = new Sales
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = item.Price,
                    SalesInvoice = salesInvoice
                };
                _context.Add(sale);
            }
            _context.Add(salesInvoice);
            Orders orders = new Orders
            {
                SalesInvoice = salesInvoice,
                User = _context.Users.FirstOrDefault(p => p.PhoneNumber == ordersVM.PhoneNumber)
            };
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var order = await _context.Orders.FindAsync(Id);

            var sales = await _context.Sales.Where(p=>p.SalesInvoiceId==order.SalesInvoiceId).ToListAsync();

            _context.RemoveRange(sales);

            _context.Remove(order.SalesInvoice);

            _context.Remove(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }

}

