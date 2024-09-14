namespace ServiceManagement.WebAPI.DTOs
{
    public class ServiceRequestStatusDTO
    {
        public long ServiceID { get; set; }
        public int ServiceStatusID { get; set; }
        public long TechnicianID { get; set; }
        public string? TechnicianComment { get; set; }
    }

    public class ServiceRequestResponseDTO
    {
        public long ServiceID { get; set; } 
        public string? RespondMessage { get; set; }
    }
}
