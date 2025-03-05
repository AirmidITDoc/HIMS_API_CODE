using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class PrescriptionDetailsVisitWiseListDto
    {
        public long PrecriptionId {  get; set; }
        public long? OpdIpdIp { get; set; }
        public long ClassID { get; set; }
        public string? ClassName { get; set; }
        public long GenericId { get; set; }
        public string? GenericName { get; set; }
        public long DrugId { get; set; }
        public long DoseId { get; set; }
        public string? DoseName { get; set; }
        public long? Days { get; set; }
        public long InstructionId { get; set; }
        public string? InstructionDescription { get; set; }
        public string? Remark { get; set; }
        public string? DrugName { get; set; }
        public string? Instruction { get; set; }
        public double? TotalQty { get; set; }
        public double? QtyPerDay { get; set; }
        public string? PWeight { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public string? BSL { get; set; }
        public string? PHeight { get; set; }
        public string? BMI { get; set; }
        public long? PatientReferDocId { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? Examination { get; set; }
        public string? Temp { get; set; }
        public string? Advice { get; set; }
        public string? Doctorname { get; set; }
        public DateTime FollowupDate { get; set; }
        public string? SpO2 { get; set; }
        public long? DoseOption2 { get; set; }
        public long? DoseNameOption2 { get; set; }
        public long? DaysOption2 { get; set; }
        public long? DoseOption3 { get; set; }
        public long? DoseNameOption3 { get; set; }
        public long? DaysOption3 { get; set; }
    }
}
		