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
    public class SalesRepository : Repository<Sales>, ISalesRepository
    {
        private readonly ApplicationDbContext _db;
        public SalesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Sales obj)
        {
            _db.Update(obj);
        }
    }
}
