using ServiceManagement.Domain.Entities;
using ServiceManagement.EFCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces
{
    public interface IBranchesRepository: IRepositoryBase<Branches>
    {
        List<BranchWithCompany> GetBranchwithCompnay();
    }
}
