using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class Technicians
    {
        [Key]
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public IEnumerable<ServiceRequests>? ServiceRequests { get; set; }
    }
}
