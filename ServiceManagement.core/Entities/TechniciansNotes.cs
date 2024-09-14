using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class TechniciansNotes
    {
        [Key]
        public long ID { get; set; }
        public long? RequestID { get; set; }
        public long? TechnicianID { get; set; }
        public string? Notes { get; set; }
        public int StatusID { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("RequestID")]
        public ServiceRequests ServiceRequests { get; set; }

        [ForeignKey("TechnicianID")]
        public Technicians Technicians { get; set; }

        [ForeignKey("StatusID")]                        
        public ServiceStatuses ServiceStatuses { get; set; }                            
    }
}
