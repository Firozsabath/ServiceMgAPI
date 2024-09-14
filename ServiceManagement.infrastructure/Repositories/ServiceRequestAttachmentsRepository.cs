using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class ServiceRequestAttachmentsRepository:RepositoryBase<ServiceRequestAttachments>, IServiceRequestAttachmentsRepository
    {
        private readonly ApplicationDBContext db;

        public ServiceRequestAttachmentsRepository(ApplicationDBContext db): base(db)
        {
            this.db = db;
        }
    }
}
