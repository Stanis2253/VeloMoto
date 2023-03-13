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
    public class SalesInvoiceRepository : Repository<SalesInvoice>, ISalesInvoiceRepository
    {
        private readonly ApplicationDbContext _db;
        public SalesInvoiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SalesInvoice obj)
        {
            _db.SalesInvoice.Update(obj);
        }
    }
}
