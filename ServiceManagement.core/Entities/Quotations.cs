using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public  class Quotations
    {
        public long QuotationID { get; set; }
        public long CompanyID { get; set; }
        public long MachineID { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string? QuotationNum { get; set; }

    }

    public class QuotationItems
    {
        public long QuotationItemId { get; set; }
        public long QuotationId { get; set; }
        public long PartsID { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }

        public Quotations Quotation { get; set; }
    }
}
