using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class OPDRChargesDto
    {
        public long ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public DateTime? ChargesTime { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? UnitId { get; set; }
        public long? ServiceId { get; set; }
        public long? ClassId { get; set; }
        public long? TariffId { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? IsDoctorShareGenerated { get; set; }
        public byte? IsInterimBillFlag { get; set; }
        public long? IsPackage { get; set; }
        public long? PackageId { get; set; }
        public long? PackageMainChargeId { get; set; }
        public long? IsSelfOrCompanyService { get; set; }
        public decimal? CPrice { get; set; }
        public float? CQty { get; set; }
        public decimal? CTotalAmount { get; set; }
        public bool? IsComServ { get; set; }
        public bool? IsPrintCompSer { get; set; }
        public decimal? ChPrice { get; set; }
        public float? ChQty { get; set; }
        public decimal? ChTotalAmount { get; set; }
        public bool? IsBillableCharity { get; set; }
        public long? SalesId { get; set; }
        public bool? IsGenerated { get; set; }
        public bool? IsApprovedByCamp { get; set; }
        public int? WardId { get; set; }
        public int? BedId { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? CompanyServiceName { get; set; }
        public bool? IsInclusionExclusion { get; set; }
        public int? IsHospMrk { get; set; }
        public long? BillNo { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? AddedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string? classname { get; set; }
    }
}
