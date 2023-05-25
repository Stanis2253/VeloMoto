using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeloMotoAPI.Models;

namespace VeloMotoAPI.DataAccess
{
    public class ApplicationDbContext : IdentityUserContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Providers> Providers { get; set; }
        public DbSet<PurchasesInvoice> PurchasesInvoice { get; set; }
        public DbSet<SalesInvoice> SalesInvoice { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<ApplicationUsers> Users { get; set; }
        public DbSet<ApplicationRoles> Roles { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<StatusOrders> StatusOrders { get; set; }



    }
}
