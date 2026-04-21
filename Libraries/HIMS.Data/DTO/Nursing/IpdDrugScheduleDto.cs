namespace HIMS.Data.DTO.Nursing
{
    public class IpdDrugScheduleDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string GenericName { get; set; }
        public string Category { get; set; }
        public int DoseNo { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Color { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public string Route { get; set; }
        public string Freq { get; set; }
        public long RegId { get; set; }
        public string Age { get; set; }
        public string GivenBy { get; set; }
        public DateTime GivenTime { get; set; }
    }
}
