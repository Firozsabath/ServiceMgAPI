using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class Companies
    {
        [Key]
        public long ID { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedTime { get; set; }       
        public bool? isBlocked { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CompanySize { get; set; }
        public string? Notes { get; set; }
        public int? AssignedDiscount { get; set; }

        public IEnumerable<Branches>? Branches { get; set; }
    }
}
