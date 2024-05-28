using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.ViewModels
{
    public class Techniciantickets
    {
        public long technicianID { get; set; }
        public  string name { get; set; }
        public int tickets { get; set; }
        public int onhold { get; set; }
        public int overduetickets { get; set; }
    }
}
