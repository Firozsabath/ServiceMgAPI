using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class QuotationRepository:RepositoryBase<Quotations>, IQuotationRepository
    {
        private readonly ApplicationDBContext db;

        public QuotationRepository(ApplicationDBContext db):base(db)
        {
            this.db = db;
        }
    }
}
