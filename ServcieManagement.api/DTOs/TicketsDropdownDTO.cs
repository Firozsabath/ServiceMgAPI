using ServiceManagement.Domain.Entities;

namespace ServiceManagement.WebAPI.DTOs
{
    public class TicketsDropdownDTO
    {
        public IEnumerable<RequestTypes> RequestTypes { get; set; }
        public IEnumerable<PriorityLevels> Priorities { get; set; }
        public IEnumerable<ServiceStatuses> Statuses { get; set; }
    }

    public class TicketsReportDropdownsDTO {
        public IEnumerable<RequestTypes> RequestTypes { get; set; }
        public IEnumerable<PriorityLevels> Priorities { get; set; }
        public IEnumerable<ServiceStatuses> Statuses { get; set; }
        public IEnumerable<ticketsDrpdwn> RequestCompanies { get; set; }
        public IEnumerable<ticketsDrpdwn> RequestBranches { get; set; }
        public IEnumerable<ticketsDrpdwn> RequestMachines { get; set; }
        public IEnumerable<ticketsDrpdwn> RequestTechnicians { get; set; }

    }


    public class ticketsDrpdwn {
        public long ID { get; set; }
        public string Name { get; set; }

    }
    
}
