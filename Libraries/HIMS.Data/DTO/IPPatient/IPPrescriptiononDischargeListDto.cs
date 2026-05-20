namespace HIMS.Data.DTO.IPPatient
{
    public class IPPrescriptiononDischargeListDto
    {
        public long? OPD_IPD_ID { get; set; }
        public long? ItemID { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public string? Instruction { get; set; }
        public string? ItemName { get; set; }
        public string? DoseNameInEnglish { get; set; }
        public string? DoseName { get; set; }
        public string? ItemGenericName { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }


    }
}
