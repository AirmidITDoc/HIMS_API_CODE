namespace HIMS.Data.DTO.OPPatient
{
    public class getPrescriptionTemplateDetailsListDto
    {
        public long PresId { get; set; }
        public long PresDetId { get; set; }
        public DateTime Date { get; set; }
        public long ClassID { get; set; }
        public long Genericid { get; set; }
        public string DrugName { get; set; }
        public long DrugId { get; set; }
        public long DoseId { get; set; }
        public string DoseName { get; set; }
        public long Days { get; set; }
        public long InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }

        public bool? IsEnglishOrIsMarathi { get; set; }
        public string? GenericName { get; set; }




    }
}
