using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PurchaseInvoiceRepository : Repository<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseInvoiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseInvoice obj)
        {
            _db.PurchaseInvoice.Update(obj);
        }
    }
}
