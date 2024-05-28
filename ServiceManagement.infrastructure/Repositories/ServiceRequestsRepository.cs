using Microsoft.EntityFrameworkCore;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class ServiceRequestsRepository: RepositoryBase<ServiceRequests>, IServiceRequestsRepository
    {
        private readonly ApplicationDBContext _db;

        public ServiceRequestsRepository(ApplicationDBContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<ServiceRequestsVM> GetAllTasks()
        {
            var tasks = _db.ServiceRequests.Include(a => a.Techincian)
                .Include(a => a.ServiceStatus)
                .Include(a => a.PriorityLevel)
                .Include(a => a.RequestType)
                .Include(a => a.Machine)
                .Select(s => new ServiceRequestsVM
                {
                    ID = s.ID,
                    Technician = s.Techincian.Name,
                    Description = s.Description,
                    Subject = s.Subject,
                    Priority = s.PriorityLevel.Description,
                    ServiceStatus = s.ServiceStatus.Description,
                    ServiceType = s.RequestType.Description,
                    ComplatedDate = s.ComplatedDate,
                    CustomerFeedback = s.CustomerFeedback,
                    EstimatedCompleteDate = s.EstimatedCompleteDate,
                    RequestedDate = s.RequestedDate,
                    Machine = s.Machine.Name +'-'+ s.Machine.Manufacturer,
                    MachineID = s.MachineID,
                    PriorityID = s.PriorityID,
                    ServiceStatusID = s.ServiceStatusID,
                    ServiceTypeID = s.ServiceTypeID,
                    TechnicianID = s.TechnicianID,
                    RespondedinHours = s.RespondedinHours,
                    RespondMessage = s.RespondMessage,
                    TechnicianComment = s.TechnicianComment,
                    RequestID = s.RequestID
                }).OrderByDescending(x=>x.RequestedDate).ToList();
            return tasks;
        }

        public IEnumerable<ChartVM> GetByCategory()
        {
            var requests = _db.RequestTypes.Include(s => s.MyProperty).
                Select(a => new ChartVM
                {
                    name = a.Description,
                    value = a.MyProperty.Count().ToString()
                });
            return requests;
        }

        public IEnumerable<ChartVM> GetByStatus()
        {
            var requests = _db.ServiceStatuses.Include(s => s.ServiceRequests).
                Select(a => new ChartVM
                {
                    name = a.Description,
                    value = a.ServiceRequests.Count().ToString()
                });
            return requests;
        }

        public IEnumerable<ServiceRequestsVM> GetByTechnician(long id)
        {
            var tasks = _db.ServiceRequests.Include(a => a.Techincian)
                .Include(a => a.ServiceStatus)
                .Include(a => a.PriorityLevel)
                .Include(a => a.RequestType)
                .Include(a => a.Machine).Where(x=>x.TechnicianID == id)
                .Select(s => new ServiceRequestsVM
                {
                    ID = s.ID,
                    Technician = s.Techincian.Name,
                    Description = s.Description,
                    Subject = s.Subject,
                    Priority = s.PriorityLevel.Description,
                    ServiceStatus = s.ServiceStatus.Description,
                    ServiceType = s.RequestType.Description,
                    ComplatedDate = s.ComplatedDate,
                    CustomerFeedback = s.CustomerFeedback,
                    EstimatedCompleteDate = s.EstimatedCompleteDate,
                    RequestedDate = s.RequestedDate,
                    Machine = s.Machine.Name + '-' + s.Machine.Manufacturer,
                    MachineID = s.MachineID,
                    PriorityID = s.PriorityID,
                    ServiceStatusID = s.ServiceStatusID,
                    ServiceTypeID = s.ServiceTypeID,
                    TechnicianID = s.TechnicianID,
                    RespondedinHours = s.RespondedinHours,
                    RespondMessage = s.RespondMessage,
                    TechnicianComment = s.TechnicianComment
                }).OrderByDescending(x => x.RequestedDate).ToList();
            return tasks;
        }

        public ServiceRequestsVM GetByTicketID(long id)
        {
            var task = _db.ServiceRequests.Include(a => a.Techincian)
               .Include(a => a.ServiceStatus)
               .Include(a => a.PriorityLevel)
               .Include(a => a.RequestType)
               .Include(a => a.Machine).Where(x=>x.ID == id)
               .Select(s => new ServiceRequestsVM
               {
                   ID = s.ID,
                   Technician = s.Techincian.Name,
                   Description = s.Description,
                   Subject = s.Subject,
                   Priority = s.PriorityLevel.Description,
                   ServiceStatus = s.ServiceStatus.Description,
                   ServiceType = s.RequestType.Description,
                   ComplatedDate = s.ComplatedDate,
                   CustomerFeedback = s.CustomerFeedback,
                   EstimatedCompleteDate = s.EstimatedCompleteDate,
                   RequestedDate = s.RequestedDate,
                   Machine = s.Machine.Name + '-' + s.Machine.Manufacturer,
                   MachineID = s.MachineID,
                   PriorityID = s.PriorityID,
                   ServiceStatusID = s.ServiceStatusID,
                   ServiceTypeID = s.ServiceTypeID,
                   TechnicianID = s.TechnicianID,
                   RespondedinHours = s.RespondedinHours,
                   RespondMessage = s.RespondMessage,
                   TechnicianComment = s.TechnicianComment,
                   RequestID = s.RequestID
               }).FirstOrDefault();
            return task;
        }

        public IEnumerable<ServiceRequestsVM> GetFIlteredRequests(int? companyID = null, int? branchID = null, int? machineID = null,
            int? technicianID=null, int? statusID = null)
        {             
            var tasks = _db.ServiceRequests.Include(x => x.Machine)
                .ThenInclude(x => x.Branch)
                .ThenInclude(x => x.Machines).Include(x => x.Techincian)
                .Include(x => x.ServiceStatus)
                .Include(x => x.PriorityLevel)
                .Include(x => x.RequestType)
                .Where(sr => (!companyID.HasValue || sr.Machine.Branch.CompanyID == companyID.Value) &&
                        (!branchID.HasValue || sr.Machine.BranchID == branchID.Value) && (!machineID.HasValue || sr.MachineID == machineID.Value) &&
                        (!technicianID.HasValue || sr.TechnicianID == technicianID.Value) && (!statusID.HasValue || sr.ServiceStatusID == statusID.Value)).ToList();
           
            var filteresTasks = tasks.Select(s => new ServiceRequestsVM
            {
                ID = s.ID,
                Technician = s.Techincian.Name,
                Description = s.Description,
                Subject = s.Subject,
                Priority = s.PriorityLevel.Description,
                ServiceStatus = s.ServiceStatus.Description,
                ServiceType = s.RequestType.Description,
                ComplatedDate = s.ComplatedDate,
                CustomerFeedback = s.CustomerFeedback,
                EstimatedCompleteDate = s.EstimatedCompleteDate,
                RequestedDate = s.RequestedDate,
                Machine = s.Machine.Name + '-' + s.Machine.Manufacturer,
                MachineID = s.MachineID,
                PriorityID = s.PriorityID,
                ServiceStatusID = s.ServiceStatusID,
                ServiceTypeID = s.ServiceTypeID,
                TechnicianID = s.TechnicianID,
                RespondedinHours = s.RespondedinHours,
                RespondMessage = s.RespondMessage,
                TechnicianComment = s.TechnicianComment,
                RequestID = s.RequestID
            }).OrderByDescending(x => x.RequestedDate).ToList();

            return filteresTasks;

        }
    }
}
