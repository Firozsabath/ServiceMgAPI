using Microsoft.EntityFrameworkCore;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class MachinesRepository: RepositoryBase<Machines>, IMachinesRepository
    {
        private readonly ApplicationDBContext _db;

        public MachinesRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MachinesVM>> GetAllMachines()
        {
            var machines = _db.Machines.
                Include(a=>a.ContractType).Include(a=>a.Branch).
                Select(m=> new MachinesVM
                {
                    ID = m.ID,
                    BranchID = m.BranchID,
                    BranchName  = m.Branch.BranchName,
                    ContractEndDate = m.ContractEndDate,
                    ContractType = m.ContractType.Description,
                    ContractTypeID = m.ContractTypeID, 
                    CreatedTime = m.CreatedTime,
                    InstallationDate = m.InstallationDate,
                    MachineUniqueID = m.MachineUniqueID,
                    Manufacturer = m.Manufacturer,
                    Model = m.Model,
                    Name = m.Name,
                    UpdatedTime = m.UpdatedTime,
                    SkuID = m.SkuID
                }).ToList();
            return machines;

        }
    }
}
