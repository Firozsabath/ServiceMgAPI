using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public  class Quotations
    {
        [Key]
        public long QuotationID { get; set; }
        public long? CompanyID { get; set; }
        public long? MachineID { get; set; }
        public string? companyName { get; set; }
        public string? companyAddress { get; set; }
        public string? Trn { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string? QuotationNum { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TotalVat { get; set; }
        public string? PaymentTerms { get; set; }
        public IEnumerable<QuotationItems> QuotationItems { get; set; }

    }

    public class QuotationItems
    {
        [Key]
        public long QuotationItemId { get; set; }
        public long QuotationId { get; set; }
        public long PartsID { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Vat { get; set; }
        public decimal? VatAmount { get; set; }
        public string? Availability { get; set; }

        [ForeignKey("QuotationId")]
        public Quotations Quotation { get; set; }
    }
}
