namespace ServiceManagement.WebAPI.DTOs
{
    public class ServicePartsDTO
    {
        public long ID { get; set; }
        public long PartsID { get; set; }
        public long ServiceID { get; set; }
        public int QuantityUsed { get; set; }
        public string? PartName { get; set; }
    }
}
