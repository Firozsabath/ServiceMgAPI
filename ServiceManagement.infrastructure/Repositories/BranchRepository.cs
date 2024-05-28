using Microsoft.EntityFrameworkCore;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.EFCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class BranchRepository: RepositoryBase<Branches>, IBranchesRepository
    {
        private readonly ApplicationDBContext _db;

        public BranchRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }

        public List<BranchWithCompany> GetBranchwithCompnay()
        {
            var branch = new List<BranchWithCompany>();
            branch = _db.Branches.Include(b=>b.Company).Select(branch=>new BranchWithCompany { branchID = branch.ID,branchName = branch.BranchName, CompanyName = branch.Company.Name}).ToList();
            return branch;
        }
    }
}
