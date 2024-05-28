using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class CompanyRepository: RepositoryBase<Companies>, ICompanyRepository
    {
        private ApplicationDBContext _db;

        public CompanyRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    }
}
