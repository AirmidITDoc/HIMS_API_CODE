using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ConfigSetting
    {
        public long ConfigId { get; set; }
        public long? PrintRegAfterReg { get; set; }
        public string? Ipdprefix { get; set; }
        public long? Otcharges { get; set; }
        public long? PrintOpdcaseAfterVisit { get; set; }
        public long? PrintIpdafterAdm { get; set; }
        public long? PopOpbillAfterVisit { get; set; }
        public long? PopPayAfterOpbill { get; set; }
        public long? GenerateOpbillInCashOption { get; set; }
        public long? MandatoryFirstName { get; set; }
        public long? MandatoryMiddleName { get; set; }
        public long? MandatoryLastName { get; set; }
        public long? MandatoryAddress { get; set; }
        public long? MandatoryCity { get; set; }
        public long? MandatoryAge { get; set; }
        public long? MandatoryPhoneNo { get; set; }
        public long? OpdBillingCounterId { get; set; }
        public long? OpdReceiptCounterId { get; set; }
        public long? OpdRefundBillCounterId { get; set; }
        public long? OpdAdvanceCounterId { get; set; }
        public long? OpdRefundAdvanceCounterId { get; set; }
        public long? IpdAdvanceCounterId { get; set; }
        public long? IpdBillingCounterId { get; set; }
        public long? IpdReceiptCounterId { get; set; }
        public long? IpdRefundOfBillCounterId { get; set; }
        public long? IpdRefundOfAdvanceCounterId { get; set; }
        public string? RegPrefix { get; set; }
        public string? RegNo { get; set; }
        public string? Ipprefix { get; set; }
        public string? Ipno { get; set; }
        public string? Opprefix { get; set; }
        public string? Opno { get; set; }
        public long? PathDepartment { get; set; }
        public long? IsPathologistDr { get; set; }
        public string? LabSampleNo { get; set; }
        public long? PatientTypeSelf { get; set; }
        public long? PharmacySalesCounterId { get; set; }
        public long? PharmacySalesReturnCounterId { get; set; }
        public long? PharmacyReceiptCounterId { get; set; }
        public bool? ClassForEdit { get; set; }
        public long? AnesthetishId { get; set; }
        public long? NeroSurgeonId { get; set; }
        public long? GeneralSurgeonId { get; set; }
        public bool? DateInterval { get; set; }
        public int? DateIntervalDays { get; set; }
        public long? MemberNoG { get; set; }
        public long? BarCodeSeqNo { get; set; }
        public long? GrnpartyCounterId { get; set; }
        public long? CantenCashId { get; set; }
        public long? CantenPayCashId { get; set; }
        public long? PharIpadvCounterId { get; set; }
        public long? PharStrId { get; set; }
        public bool? ChkPharmacyDue { get; set; }
        public long? CompBillNo { get; set; }
        public long? PharServiceIdToTranfer { get; set; }
        public string? FilePathLocation { get; set; }
        public long? IpnoEmg { get; set; }
        public long? IpdayCareNo { get; set; }
        public bool? GIsPharmacyPaperSetting { get; set; }
        public string? GPharmacyPrintName { get; set; }
        public string? GPharmacyPaperName { get; set; }
        public string? GIsOppaperSetting { get; set; }
        public string? GOpprintName { get; set; }
        public string? GOppaperName { get; set; }
        public bool? GIsIppaperSetting { get; set; }
        public string? GIpprintName { get; set; }
        public string? GIppaperName { get; set; }
    }
}
