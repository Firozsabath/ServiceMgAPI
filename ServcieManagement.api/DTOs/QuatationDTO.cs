namespace ServiceManagement.WebAPI.DTOs
{
    public class QuatationDTO
    {
        public int quotationID { get; set; }
        public int companyID { get; set; }
        public DateTime? dateCreated { get; set; }
        public decimal totalAmount { get; set; }
        public DateTime? validUntil { get; set; }
        public string? quotationNum {  get; set; }

        public List<QuotationitemsDTO> quotationItems { get; set; }
    }

    public class QuotationitemsDTO
    {
        public int partsId { get; set; }
        public int quantityUsed { get; set; }
    }

    public class QuotationitemDetailssDTO
    {
        public long partsId { get; set; }
        public string Description { get; set; }
        public int quantityUsed { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Total { get; set; }        
    }

    public class QuoatationConvertedDTO
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set;}
        public decimal? totalAmount { get; set; }
        public DateTime? validUntil { get; set; }
        public DateTime? dateCreated { get; set; }

        public List<QuotationitemDetailssDTO> quotationItems { get; set; }

    }

}
