using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationSpecificController : ControllerBase
    {
        private readonly IDropdowns dropdowns;
        private readonly IMachinesRepository machines;
        private readonly IBranchesRepository branches;
        private readonly ICompanyRepository company;
        private readonly ITechniciansRepository technicians;

        public ApplicationSpecificController(IDropdowns dropdowns, 
            IMachinesRepository machines, 
            IBranchesRepository branches, 
            ICompanyRepository company, 
            ITechniciansRepository technicians)
        {
            this.dropdowns = dropdowns;
            this.machines = machines;
            this.branches = branches;
            this.company = company;
            this.technicians = technicians;
        }
        [HttpGet("GetTicketsDropdowns")]
        public IActionResult GetTicketsDropdowns()
        {
            var dropdowns = new TicketsDropdownDTO
            {
                Priorities = this.dropdowns.Priorities(),
                RequestTypes = this.dropdowns.RequestTypes(),
                Statuses = this.dropdowns.ServiceStatuses()
            };
            return Ok(dropdowns);
        }

        [HttpGet("ContractTypes")]
        public IActionResult GetContractTypes() {

            return Ok(this.dropdowns.ContractTypes());
        }

        [HttpGet("GetDropsForrequestsReport")]
        public IActionResult GetDrops() {

            var comp = this.company.GetAll().Select(c => new ticketsDrpdwn
            {
                ID = c.ID,
                Name = c.Name
            });

            var branchlist = this.branches.GetAll(null, b => b.Company).Select(b => new ticketsDrpdwn
            {
                ID = b.ID,
                Name = b.BranchName+' '+'-'+' '+ b.Company.Name
            }) ;

            var machinelist = this.machines.GetAll().Select(b => new ticketsDrpdwn
            {
                ID = b.ID,
                Name = b.Name
            });

            var technicianlist = this.technicians.GetAll().Select(b => new ticketsDrpdwn
            {
                ID = b.ID,
                Name = b.Name
            });

            var dropdowns = new TicketsReportDropdownsDTO
            {
                Priorities = this.dropdowns.Priorities(),
                RequestTypes = this.dropdowns.RequestTypes(),
                Statuses = this.dropdowns.ServiceStatuses(),
                RequestCompanies = comp,
                RequestBranches = branchlist,
                RequestMachines = machinelist,
                RequestTechnicians  = technicianlist
            };

            return Ok(dropdowns);
        }
    }
}
