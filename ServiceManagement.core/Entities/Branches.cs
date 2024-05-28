using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class Branches
    {
        [Key]
        public long ID { get; set; }
        public long? CompanyID { get; set; }
        public string? BranchName { get; set; }
        public string? ContactNumber { get; set; }
        public string? TrnNumber { get; set; }
        public string? ContactPerson { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }        
        public string? doc1 { get; set; }
        public string? doc2 { get; set; }

        [ForeignKey("CompanyID")]
        public Companies? Company { get; set; }

        public IEnumerable<Machines>? Machines { get; set; }

    }
}
