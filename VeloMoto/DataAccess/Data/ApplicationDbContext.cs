
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        { 

        }

        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<StatusOrder> StatusOrder { get; set; }
        public DbSet<SalesInvoice> SalesInvoice { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoice { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Images> Images { get; set; }

    }
 }

