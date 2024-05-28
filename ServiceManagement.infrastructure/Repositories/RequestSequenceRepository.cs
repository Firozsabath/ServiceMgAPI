using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class RequestSequenceRepository : IRequestSequenceRepository
    {
        private readonly ApplicationDBContext db;

        public RequestSequenceRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public int GetNextTicketNumber()
        {
            int nextRequestNumber = 0;
            var sequence = this.db.RequestSequence.FirstOrDefault();
            if (sequence == null)
            {
                throw new Exception("Request sequence not initialized.");
            }

            nextRequestNumber = sequence.LatestRequestNumber + 1;
            sequence.LatestRequestNumber = nextRequestNumber;
            this.db.SaveChanges();

            return nextRequestNumber;
        }

        public void UpdateLatestTicketNumber(int latestTicketNumber)
        {
            throw new NotImplementedException();
        }
    }
}
