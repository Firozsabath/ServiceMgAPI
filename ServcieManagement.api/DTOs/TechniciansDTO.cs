namespace ServiceManagement.WebAPI.DTOs
{
    public class TechniciansDTO
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }

    public class TechnicianRespnseViolation
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public int ticketCount { get; set; }
        public int responseViolations { get; set; }
    }
}
