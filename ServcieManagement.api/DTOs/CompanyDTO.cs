namespace ServiceManagement.WebAPI.DTOs
{
    public class CompanyDTO
    {
        public long ID { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? isBlocked { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CompanySize { get; set; }
        public string? Notes { get; set; }
    }
}
