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
    public class StatusOrderRepository : Repository<StatusOrder>, IStatusOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public StatusOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StatusOrder obj)
        {
            _db.StatusOrder.Update(obj);
        }
    }
}
