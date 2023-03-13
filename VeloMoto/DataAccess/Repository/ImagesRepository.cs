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
    public class ImagesRepository : Repository<Images>, IImagesRepository
    {
        private readonly ApplicationDbContext _db;
        public ImagesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Images obj)
        {
            _db.Images.Update(obj);
        }
    }
}
