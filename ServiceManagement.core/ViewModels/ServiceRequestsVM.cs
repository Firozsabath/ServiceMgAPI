using ServiceManagement.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.ViewModels
{
    public class ServiceRequestsVM
    {
        public long ID { get; set; }
        public string? Machine { get; set; }
        public string? Technician { get; set; }
        public long? MachineID { get; set; }
        public long? TechnicianID { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ComplatedDate { get; set; }
        public DateTime? EstimatedCompleteDate { get; set; }
        public string? Description { get; set; }
        public string? Subject { get; set; }
        public string? CustomerFeedback { get; set; }
        public string? ServiceType { get; set; }
        public string? Priority { get; set; }
        public string? ServiceStatus { get; set; }
        public int? ServiceTypeID { get; set; }
        public int? PriorityID { get; set; }
        public int? ServiceStatusID { get; set; }
        public double? RespondedinHours { get; set; }
        public string? RespondMessage { get; set; }
        public string? TechnicianComment { get; set; }
        public string? RequestID { get; set; }
        public IList<TechnicianNotesVM>? Notes { get; set; }
        public IList<ServiceRequestAttachments>? Attachments { get; set; }

    }

    public class TechnicianNotesVM
    {
        public string Status { get; set; }
        public string TechnicianName { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class ChartVM
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    
}
