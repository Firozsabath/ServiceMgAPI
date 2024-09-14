using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Machines> Machines { get; set; }
        public DbSet<Technicians> Technicians { get; set; }
        public DbSet<ServiceRequests> ServiceRequests { get; set; }
        public DbSet<ServiceRequestAttachments> ServiceRequestAttachments { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<RequestTypes> RequestTypes { get; set; }
        public DbSet<PriorityLevels> PriorityLevels { get; set; }
        public DbSet<ServiceStatuses> ServiceStatuses { get; set; }
        public DbSet<ContractTypes> ContractTypes { get; set; }
        public DbSet<ServiceParts> ServiceParts { get; set; }
        public DbSet<RequestSequence> RequestSequence { get; set; }
        public DbSet<TechniciansNotes> TechniciansNotes { get; set; }
        public DbSet<Quotations> Quotations { get; set; }
        public DbSet<QuotationItems> QuotationItems { get; set; }
    }
}
