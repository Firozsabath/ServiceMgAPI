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

        public void UpdateBranch(Branches branch)
        {
            this._db.Update(branch);
            this._db.Entry(branch).Property(b=>b.doc1).IsModified = false;
            this._db.Entry(branch).Property(b=>b.doc2).IsModified = false;            
        }

        public bool updateUploadedPath(string filepath, int branchID, string ftype)
        {
            var br = _db.Branches.FirstOrDefault(b => b.ID == branchID);
            if(br != null)
            {
                if(ftype == "doc1") { br.doc1 = filepath; }                   
                else
                {
                    br.doc2 = filepath;
                }               
                _db.Branches.Update(br);
                var st = _db.SaveChanges();
                return st > 0;
            }
            else { return false; }
                     
        }
    }
}
