using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class RequestSequence
    {
        [Key]
        public int Id { get; set; }
        public int LatestRequestNumber { get; set; }
    }
   
}
