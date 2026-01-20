using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.OutPatient
{
    public class DRBillModel
    {
        public long Drbno { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? IsCancelled { get; set; }
        public string? PbillNo { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? AddedBy { get; set; }
        public long? CashCounterId { get; set; }
        public string? BillTime { get; set; }
        public long? ConcessionReasonId { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
        public long? CompanyId { get; set; }
        public long? TariffId { get; set; }
        public long? UnitId { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? CompanyRefNo { get; set; }
        public long? ConcessionAuthorizationName { get; set; }
        public double? TaxPer { get; set; }
        public decimal? TaxAmount { get; set; }
    }
    public  class TDrbillDetModel
    {
        //public long DrbillDetId { get; set; }
        public long? Drno { get; set; }
        public long? ChargesId { get; set; }
    }
    public class TDrbillDetUpdateModel
    {
        public long DrbillDetId { get; set; }
        public long? Drno { get; set; }
        public long? ChargesId { get; set; }
    }
    public class TDraddChargeModel
    {
        public long ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public string? ChargesTime { get; set; }
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
    }
    public class TDraddChargeUpdateModel
    {
        public long ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public string? ChargesTime { get; set; }
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
        public int? ModifiedBy { get; set; }
    }
    public class DraftBillModel
        {
        public DRBillModel DRBill { get; set; }
        public List<TDrbillDetModel> TDrbillDet { get; set; }
        public List<TDraddChargeModel> TDraddCharge { get; set; }

        }
    public class DraftBillUpdateModel
    {
        public DRBillModel DRBill { get; set; }
        public List<TDrbillDetUpdateModel> TDrbillDet { get; set; }
        public List<TDraddChargeUpdateModel> TDraddCharge { get; set; }


    }
}

