using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class OtRequestSurgeryDetailListDto
    {
        public long? OtrequestId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryCategoryName { get; set; }
        public string? SurgeryName { get; set; }
        public string? SurgeryPart { get; set; }
        public DateTime? SurgeryFromTime { get; set; }
        public DateTime? SurgeryEndTime { get; set; }
        public long? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public string? AnestheticsName { get; set; }
       

    }
    public class OtRequestListDto
    {
        public long OtrequestId { get; set; }
        public DateTime? OtrequestDate { get; set; }
        public DateTime? OtrequestTime { get; set; }
        public string? OtrequestNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? BloodGroup { get; set; }
        public string? OPDNo { get; set; }
        public string? PatientName { get; set; }
        public long? CategoryType { get; set; }
        public string? TypeName { get; set; }
        public string? Comments { get; set; }
        public string? OTTableName { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public DateTime? EstimateTime { get; set; }
        public bool? RequestType { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? Infective { get; set; }
        public long? CreatedBy { get; set; }
        public string? UserName { get; set; }
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientType { get; set; }
        public string? RegNo { get; set; }
        public string? MobileNo { get; set; }
        public string? GenderName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
    }
    public class OtRequestAttendingDetailListDto
    {
        public long OtrequestId { get; set; }
        public long? DoctorTypeId { get; set; }
        public string? DoctorType { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
      
    }


}
