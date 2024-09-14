namespace ServiceManagement.WebAPI.DTOs
{
    public class ServiceRequestsDTO
    {
        public long ID { get; set; }
        public long? MachineID { get; set; }
        public long? TechnicianID { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ComplatedDate { get; set; }
        public DateTime? EstimatedCompleteDate { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? CustomerFeedback { get; set; }
        public int? ServiceTypeID { get; set; }
        public int? PriorityID { get; set; }
        public int? ServiceStatusID { get; set; }
        public DateTime? RespondTime { get; set; }
        public double? RespondedinHours { get; set; }
        public string? RespondMessage { get; set; }
        public string? TechnicianComment { get; set; }
        public string? RequestID { get; set; }
        public IFormFile[]? Documents { get; set; }
    }
}
