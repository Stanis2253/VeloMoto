using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Purchase obj)
        {
            _db.Purchase.Update(obj);
        }
    }
}
