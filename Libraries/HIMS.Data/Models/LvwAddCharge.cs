using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwAddCharge
    {
        public long ChargesId { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public string? ChargesDate { get; set; }
        public bool? IsGenerated { get; set; }
        public bool? IsCancelled { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? IsDoctorShareGenerated { get; set; }
        public byte? IsInterimBillFlag { get; set; }
        public long? IsPackage { get; set; }
        public DateTime? Cdate { get; set; }
        public long? PackageId { get; set; }
        public long? IsPackageMaster { get; set; }
        public string? ClassName { get; set; }
        public string? ChargesAddedName { get; set; }
        public string? ChargesCancelledByName { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public decimal CPrice { get; set; }
        public float CQty { get; set; }
        public decimal CTotalAmount { get; set; }
        public bool? IsComServ { get; set; }
        public bool? IsPrintCompSer { get; set; }
        public string? CompanyServiceName { get; set; }
        public long? ClassId { get; set; }
    }
}
