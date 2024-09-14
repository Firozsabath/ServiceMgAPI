using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class ServiceRequestAttachments
    {
        [Key]
        public long ID { get; set; }

        public long RequestID { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }

        [ForeignKey("RequestID")]
        public ServiceRequests ServiceRequest { get; set; }
    }
}
