namespace HIMS.Data.DTO.Administration
{
    public class GetVisitInfoListDto
    {
        public long PrecriptionId { get; set; }
        public DateTime VisitDate { get; set; }
        public long VisitId { get; set; }
        public long OPD_IPD_IP { get; set; }
        public long ClassID { get; set; }
        public string? ClassName { get; set; }
        public string? GenericId { get; set; }
        public string? GenericName { get; set; }
        public long DrugId { get; set; }
        public string? DrugName { get; set; }
        public long? DoseId { get; set; }
        public string? DoseName { get; set; }
        public string? Days { get; set; }
        public long? InstructionId { get; set; }
        public string? InstructionDescription { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public string? TotalQty { get; set; }
        public string? QtyPerDay { get; set; }
        public string? PWeight { get; set; }
        public string? Pulse { get; set; }
        public string? BP { get; set; }
        public string? BSL { get; set; }
        public string? PHeight { get; set; }
        public string? PatientReferDocId { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? Examination { get; set; }
        public string? Temp { get; set; }
        public string? Advice { get; set; }
        public string? Doctorname { get; set; }
        public string? FollowupDate { get; set; }
        public string? SpO2 { get; set; }
        public string? DoseOption2 { get; set; }
        public string? DoseNameOption2 { get; set; }
        public string? DaysOption2 { get; set; }
        public string? DoseOption3 { get; set; }
        public string? DoseNameOption3 { get; set; }
        public string? DaysOption3 { get; set; }
        public string? OPDNo { get; set; }

    }
}
