using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class Dropdowns : IDropdowns
    {
        public Dropdowns(ApplicationDBContext db)
        {
            _db = db;
        }

        public ApplicationDBContext _db { get; }

        public IEnumerable<ContractTypes> ContractTypes()
        {
            return _db.ContractTypes.ToList();
        }

        public IEnumerable<PriorityLevels> Priorities()
        {
            return _db.PriorityLevels.ToList();
        }

        public IEnumerable<RequestTypes> RequestTypes()
        {
            return _db.RequestTypes.ToList();
        }

        public IEnumerable<ServiceStatuses> ServiceStatuses()
        {
            return _db.ServiceStatuses.ToList();
        }
    }
}
