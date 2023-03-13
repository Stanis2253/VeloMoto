using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ISalesRepository : IRepository<Sales>
    {
        void Update(Sales obj);
    }
}
