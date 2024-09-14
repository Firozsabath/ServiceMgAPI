using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class RequestTypesRepository: RepositoryBase<RequestTypes>,IRequestTypesRepository
    {
        private readonly ApplicationDBContext db;

        public RequestTypesRepository(ApplicationDBContext db):base(db)
        {
            this.db = db;
        }
    }
}
