using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class ServiceStatuses
    {
        [Key]
        public int ID { get; set; }
        public string? Description { get; set; }

        public IEnumerable<ServiceRequests>? ServiceRequests { get; set; }
    }
}
