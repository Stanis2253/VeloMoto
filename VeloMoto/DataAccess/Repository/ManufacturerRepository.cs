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
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        private readonly ApplicationDbContext _db;
        public ManufacturerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Manufacturer obj)
        {
            _db.Manufacturer.Update(obj);
        }
    }
}
