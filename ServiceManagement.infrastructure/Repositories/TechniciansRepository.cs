using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class TechniciansRepository: RepositoryBase<Technicians>, ITechniciansRepository
    {
        private readonly ApplicationDBContext _db;

        public TechniciansRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }

        public IEnumerable<Techniciantickets> GettechTicketDetails()
        {
            var ticketDetails = _db.Technicians
                 .Select(t => new Techniciantickets
                 {
                     technicianID = t.ID,
                     name = t.Name,
                     tickets = t.ServiceRequests.Count(),
                     overduetickets = t.ServiceRequests.Count(s=>s.EstimatedCompleteDate < DateTime.Today && s.ServiceStatusID != 4),
                     onhold = t.ServiceRequests.Count(s=>s.ServiceStatusID == 4),
                 }).ToList();
            return ticketDetails;           

        }
    }
}
