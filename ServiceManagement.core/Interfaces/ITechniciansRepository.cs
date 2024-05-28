using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces
{
    public interface ITechniciansRepository: IRepositoryBase<Technicians>
    {
        IEnumerable<Techniciantickets> GettechTicketDetails();
    }
}
