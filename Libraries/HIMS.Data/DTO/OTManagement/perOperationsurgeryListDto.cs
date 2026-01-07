using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public  class perOperationsurgeryListDto
    {
        public long OTPreOperationId {  get; set; }
        public long SurgeryCategoryId { get; set; }
        public string SurgeryCategoryName { get; set; }
        public long SurgeryId { get; set; }
        public string SurgeryName { get; set; }
        public string SurgeryPart { get; set; }
        public string SurgeryFromTime { get; set; }
        public string SurgeryEndTime { get; set; }
        public double SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long SurgeonId { get; set; }
        public string SurgeonName { get; set; }
        public long AnesthetistId { get; set; }
        public string AnestheticsName { get; set; }
       

    }
    public class PreOperationAttendentListDto
    {
        public long OTPreOperationId { get; set; }
        public long DoctorTypeId { get; set; }
        public string DoctorType { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
       
    }
    public class OtpreOperationDiagnosisListDto
    {
        public long OtpreOperationDiagnosisDetId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }



    }
    public  class OtPreOperationCathlabDiagnosisListDto
    {
        public long OtpreOperationCathLabDiagnosisDetId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
       
    }
    public class TOtInOperationPostOperDiagnosisDto
    {
        public long OtinOperationPostOperDiagnosisDetId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }

    }
    public class TOtInOperationDiagnosisDto
    {
        public long OtinOperationDiagnosisDetId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }



    }
    public class TOtAnesthesiaPreOpdiagnosisDto
    {
        public long OtanesthesiaPreOpdiagnosisId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
    }
}
