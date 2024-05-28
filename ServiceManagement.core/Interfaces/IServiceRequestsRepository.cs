using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces
{
    public interface IServiceRequestsRepository:IRepositoryBase<ServiceRequests>
    {
        IEnumerable<ServiceRequestsVM> GetAllTasks();
        ServiceRequestsVM GetByTicketID(long id);
        IEnumerable<ServiceRequestsVM> GetByTechnician(long id);
        IEnumerable<ChartVM> GetByCategory();        
        IEnumerable<ChartVM> GetByStatus();
        IEnumerable<ServiceRequestsVM> GetFIlteredRequests(int? companyID=null, int? branchID = null, int ? machineID=null, int? technicianID = null, int? statusID = null);

    }
}
