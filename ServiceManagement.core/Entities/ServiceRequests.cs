using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class ServiceRequests
    {
        [Key]
        public long ID { get; set; }
        public long? MachineID { get; set; }
        public long? TechnicianID { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ComplatedDate { get; set; }
        public DateTime? EstimatedCompleteDate { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? CustomerFeedback { get; set; }
        public int? ServiceTypeID { get; set; }
        public int? PriorityID { get; set; }
        public int? ServiceStatusID { get; set; }
        public DateTime? RespondTime { get; set; }
        public double? RespondedinHours { get; set; }
        public string? RespondMessage { get; set; }
        public string? TechnicianComment { get; set; }
        public string? RequestID { get; set; }


        [ForeignKey("MachineID")]
        public Machines? Machine { get; set; }

        [ForeignKey("TechnicianID")]
        public Technicians? Techincian { get; set; }

        [ForeignKey("ServiceTypeID")]
        public RequestTypes? RequestType { get; set; }

        [ForeignKey("PriorityID")]
        public PriorityLevels? PriorityLevel { get; set; }

        [ForeignKey("ServiceStatusID")]
        public ServiceStatuses? ServiceStatus { get; set; }

        public IEnumerable<ServiceParts>? ServiceParts { get; set; }
        public IEnumerable<TechniciansNotes>? TechniciansNotes { get; set; }
        public IEnumerable<ServiceRequestAttachments>? ServiceRequestAttachments { get; set; }
    }
}
