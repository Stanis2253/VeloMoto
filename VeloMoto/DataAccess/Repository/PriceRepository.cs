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
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        private readonly ApplicationDbContext _db;
        public PriceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
