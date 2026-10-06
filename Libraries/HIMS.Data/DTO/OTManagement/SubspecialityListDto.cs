namespace HIMS.Data.DTO.OTManagment
{
    public class SubspecialityListDto
    {
        public long SubSpecialtyId { get; set; }
        public string? SubSpecialtyName { get; set; }
        public long SpecialtyID { get; set; }
        public bool IsActive { get; set; }
        public string? SpecialtyName { get; set; }

    }
}