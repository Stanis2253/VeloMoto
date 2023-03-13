﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IPurchaseInvoiceRepository : IRepository<PurchaseInvoice>
    {
        void Update(PurchaseInvoice obj);
    }
}
