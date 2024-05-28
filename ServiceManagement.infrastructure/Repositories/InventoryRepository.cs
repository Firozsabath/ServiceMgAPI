using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class InventoryRepository: RepositoryBase<Inventory>, IInventroryRepository
    {
        private readonly ApplicationDBContext _db;

        public InventoryRepository(ApplicationDBContext db): base(db)
        {
            _db = db;
        }
    }
}
