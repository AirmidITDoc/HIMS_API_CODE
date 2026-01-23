namespace HIMS.API.Models.Pathology
{
    public class LabPatientPersonInfoModel
    {
        public long PatientInfoId { get; set; }
        public long? UnitId { get; set; }
        public long? PatientId { get; set; }
        public string? EmailIdOrMobileNo { get; set; }
        public string? Comments { get; set; }
    }
}
