using VeloMotoAPI.DataAccess.Interfaces;
using VeloMotoAPI.DataAccess.Repositories;
using VeloMotoAPI.Models;

namespace VeloMotoAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Products> _products;
        private IGenericRepository<Categories> _categories;
        private IGenericRepository<Manufacturers> _manufacturers;
        private IGenericRepository<Orders> _orders;
        private IGenericRepository<Sales> _sales;
        private IGenericRepository<SalesInvoice> _salesInvoices;
        private IGenericRepository<Prices> _prices;
        private IGenericRepository<Images> _images;
        private IGenericRepository<Providers> _providers;
        private IGenericRepository<Purchases> _purchases;
        private IGenericRepository<PurchasesInvoice> _purchasesInvoices;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Products> Products => 
            _products ??= new GenericRepository<Products>(_context);

        public IGenericRepository<Categories> Categories =>
            _categories ??= new GenericRepository<Categories>(_context);

        public IGenericRepository<Manufacturers> Manufacturers =>
            _manufacturers ??= new GenericRepository<Manufacturers>(_context);

        public IGenericRepository<Orders> Orders =>
            _orders ??= new GenericRepository<Orders>(_context);

        public IGenericRepository<Sales> Sales =>
            _sales ??= new GenericRepository<Sales>(_context);

        public IGenericRepository<SalesInvoice> SalesInvoices =>
            _salesInvoices ??= new GenericRepository<SalesInvoice>(_context);

        public IGenericRepository<Prices> Prices =>
            _prices ??= new GenericRepository<Prices>(_context);

        public IGenericRepository<Images> Images =>
            _images ??= new GenericRepository<Images>(_context);

        public IGenericRepository<Providers> Providers =>
            _providers ??= new GenericRepository<Providers>(_context);

        public IGenericRepository<Purchases> Purchases =>
            _purchases ??= new GenericRepository<Purchases>(_context);

        public IGenericRepository<PurchasesInvoice> PurchasesInvoices =>
            _purchasesInvoices ??= new GenericRepository<PurchasesInvoice>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 