using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemMaster
    {
        public long ItemId { get; set; }
        public string? ItemShortName { get; set; }
        public string? ItemName { get; set; }
        public long? ItemTypeId { get; set; }
        public long? ItemCategaryId { get; set; }
        public long? ItemGenericNameId { get; set; }
        public long? ItemClassId { get; set; }
        public long? PurchaseUomid { get; set; }
        public long? StockUomid { get; set; }
        public string? ConversionFactor { get; set; }
        public long? CurrencyId { get; set; }
        public double? TaxPer { get; set; }
        public bool? Isdeleted { get; set; }
        public long? Addedby { get; set; }
        public long? UpDatedBy { get; set; }
        public bool? IsBatchRequired { get; set; }
        public float? MinQty { get; set; }
        public float? MaxQty { get; set; }
        public float? ReOrder { get; set; }
        public string? Hsncode { get; set; }
        public double? Cgst { get; set; }
        public double? Sgst { get; set; }
        public double? Igst { get; set; }
        public long? ManufId { get; set; }
        public bool? IsNarcotic { get; set; }
        public bool? IsH1drug { get; set; }
        public bool? IsScheduleH { get; set; }
        public bool? IsHighRisk { get; set; }
        public bool? IsScheduleX { get; set; }
        public bool? IsLasa { get; set; }
        public bool? IsEmgerency { get; set; }
        public long? DrugType { get; set; }
        public string? DrugTypeName { get; set; }
        public string? ProdLocation { get; set; }
        public long? ItemCompnayId { get; set; }
        public DateTime? IsCreatedBy { get; set; }
        public DateTime? IsUpdatedBy { get; set; }
    }
}
