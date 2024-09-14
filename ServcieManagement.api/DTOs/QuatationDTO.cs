namespace ServiceManagement.WebAPI.DTOs
{
    public class QuatationDTO
    {
        public int quotationID { get; set; }
        public int companyID { get; set; }
        //public int machineID { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string trn { get; set; }
        public DateTime? dateCreated { get; set; }
        public decimal totalAmount { get; set; }
        public decimal subTotal { get; set; }
        public decimal totalVat { get; set; }
        public DateTime? validUntil { get; set; }
        public string? quotationNum {  get; set; }
        public decimal? serviceCharges {  get; set; }
        public string? paymentTerms {  get; set; }

        public List<QuotationitemsDTO> quotationItems { get; set; } //Don't touch unless you are sure.
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
        public int? vatPercent { get; set; }
        public decimal? vat { get; set; }
        public string? availablility { get; set; }

    }

    public class QuoatationConvertedDTO
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set;}
        public decimal? subTotal { get; set; }
        public decimal? totalVat { get; set; }
        public string? Trn { get; set; }
        public decimal? totalAmount { get; set; }
        public DateTime? validUntil { get; set; }
        public DateTime? dateCreated { get; set; }
        public string? quotationNum { get; set; }
        public decimal? serviceCharges { get; set; }
        public string? paymentTerms { get; set; }
        public List<QuotationitemDetailssDTO> quotationItems { get; set; }

    }

}
