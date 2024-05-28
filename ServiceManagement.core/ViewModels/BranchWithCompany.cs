using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.ViewModels
{
    public class BranchWithCompany
    {
        public long branchID { get; set; }
        public string? branchName { get; set; }
        public string? CompanyName { get; set; }
    }
}
