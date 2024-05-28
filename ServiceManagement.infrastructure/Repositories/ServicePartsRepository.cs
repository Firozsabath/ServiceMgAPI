using Microsoft.EntityFrameworkCore;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class ServicePartsRepository: RepositoryBase<ServiceParts>, IservicePartsRepository
    {
        private readonly ApplicationDBContext db;

        public ServicePartsRepository(ApplicationDBContext db):base(db)
        {
            this.db = db;
        }
    }
}
