using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Entities
{
    public class Inventory
    {
        [Key]
        public long PartsID { get; set; }
        public string? Description { get; set; }
        public int? CategoryID { get; set; }
        public string? Manufacturer { get; set; }
        public string? Supplier { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? MiscellaniousCost { get; set; }
        public string? MiscellaniousChargeDescription { get; set; }
        public decimal? ProductCost { get; set; }
        public int? QuantityOnHand { get; set; }
        public int? ReorderLevel { get; set; }
        public int? LeadTime { get; set; }
        public int? LastReorderQuantity { get; set; }
        public DateTime? LastReorderDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? Vendorid { get; set; }
        public string? SkuID { get; set; }
        public bool? IsVatApplied { get; set; }
        public int? VatPercent { get; set; }

        [ForeignKey("Vendorid")]
        public virtual Vendors? vendor { get; set; }
        public IEnumerable<ServiceParts>? ServiceParts { get; set; }

    }
}
