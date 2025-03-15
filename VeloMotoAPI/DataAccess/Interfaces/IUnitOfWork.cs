using VeloMotoAPI.Models;

namespace VeloMotoAPI.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Products> Products { get; }
        IGenericRepository<Categories> Categories { get; }
        IGenericRepository<Manufacturers> Manufacturers { get; }
        IGenericRepository<Orders> Orders { get; }
        IGenericRepository<Sales> Sales { get; }
        IGenericRepository<SalesInvoice> SalesInvoices { get; }
        IGenericRepository<Prices> Prices { get; }
        IGenericRepository<Images> Images { get; }
        IGenericRepository<Providers> Providers { get; }
        IGenericRepository<Purchases> Purchases { get; }
        IGenericRepository<PurchasesInvoice> PurchasesInvoices { get; }
        
        Task<int> SaveChangesAsync();
    }
} 