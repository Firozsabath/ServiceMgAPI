using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.ViewModels
{
    public class EmailB
    {
        public string Emailto { get; set; }
        public string Emailfrom { get; set; }
        public string[] Emailcc { get; set; }
        public string Emailbody { get; set; }
        public string Subject { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}
