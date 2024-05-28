using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.ViewModels
{
    public class MachinesVM
    {
        public long ID { get; set; }
        public long? BranchID { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? ContractTypeID { get; set; }
        public string? MachineUniqueID { get; set; }
        public string? ContractType { get; set;}
        public string? BranchName { get; set;}
        public string SkuID { get; set; }

    }
}
