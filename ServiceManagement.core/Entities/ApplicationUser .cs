using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        //public string? FirstName { get; set; } 
        //public string? LastName { get; set; } 
    }

    public class ApplicationRole : IdentityRole
    {
        //public string? Description { get; set; }

    }
}
