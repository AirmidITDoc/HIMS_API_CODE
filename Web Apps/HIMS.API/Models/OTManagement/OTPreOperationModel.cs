using HIMS.API.Models.Inventory.Masters;
using HIMS.Data.Models;

namespace HIMS.API.Models.OTManagement
{
    public class OTPreOperationModel
    {
        public long OtpreOperationId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtpreOperationDate { get; set; }
        public string? OtpreOperationTime { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public int? Duration { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public int? BloodArranged { get; set; }
        public int? Pacrequired { get; set; }
        public int? EquipmentsRequired { get; set; }
        public int? Infective { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public List<TOtPreOperationAttendingDetailModel> TOtPreOperationAttendingDetails { get; set; }
        public List<TOtPreOperationCathlabDiagnosisModel> TOtPreOperationCathlabDiagnoses { get; set; }
        public List<TOtPreOperationDiagnosisModel> TOtPreOperationDiagnoses { get; set; }
        public List<TOtPreOperationSurgeryDetailModel> TOtPreOperationSurgeryDetails { get; set; }


    }
    public  class TOtPreOperationAttendingDetailModel
    {
        public long OtpreOperationAttendingDetId { get; set; }
        public long? OtpreOperationId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }
       

    }
    public  class TOtPreOperationCathlabDiagnosisModel
    {
        public long OtpreOperationCathLabDiagnosisDetId { get; set; }
        public long? OtpreOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
      
    }
    public  class TOtPreOperationDiagnosisModel
    {
        public long OtpreOperationDiagnosisDetId { get; set; }
        public long? OtpreOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
      

    }
    public  class TOtPreOperationSurgeryDetailModel
    {
        public long OtpreOperationSurgeryDetId { get; set; }
        public long? OtpreOperationId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryPart { get; set; }
        public string? SurgeryFromTime { get; set; }
        public string? SurgeryEndTime { get; set; }
        public int? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public int? SeqNo { get; set; }
      

    }
}
