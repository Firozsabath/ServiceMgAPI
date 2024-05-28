using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class Vendors
    {
        [Key]
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactName { get; set; }
        public string? ContactNumber { get; set; }
        public string? ContactEmail { get; set; }

        public IEnumerable<Inventory>? Inventories { get; set; }
    }
}
