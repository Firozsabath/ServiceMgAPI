namespace ServiceManagement.WebAPI.DTOs
{
    public class InventoryVMDTO
    {
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
        public string VendorName { get; set; }
        public string skuID { get; set; }
        public bool? IsVatApplied { get; set; }
        public int? VatPercent { get; set; }
    }
}
