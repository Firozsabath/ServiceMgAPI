using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class ServiceParts
    {
        [Key]
        public long ID { get; set; }
        public long PartsID { get; set; }
        public long ServiceID { get; set; }
        public int QuantityUsed { get; set; }

        [ForeignKey("PartsID")]
        public Inventory? Parts { get; set; }

        [ForeignKey("ServiceID")]
        public ServiceRequests? requests { get; set; }
        
    }
}
