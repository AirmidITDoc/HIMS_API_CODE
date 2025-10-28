namespace HIMS.Data.Models
{
    public partial class MLvwRetrievePathologyResultUpdateIpageWise
    {
        public long? TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? SubTestId { get; set; }
        public string SubTestName { get; set; } = null!;
        public string? SubTestNamePrint { get; set; }
        public string? ParameterName { get; set; }
        public long ParameterId { get; set; }
        public string? PrintParameterName { get; set; }
        public string? ParameterShortName { get; set; }
        public string Formula { get; set; } = null!;
        public string? ResultValue { get; set; }
        public string? NormalRange { get; set; }
        public long? PrintOrder { get; set; }
        public long? PisNumeric { get; set; }
        public long? PathReportId { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? PatientName { get; set; }
        public string? ConsultantDocName { get; set; }
        public string? AgeYear { get; set; }
        public string? RegNo { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? PathResultDrName { get; set; }
        public long? PathResultDr1 { get; set; }
        public string? SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public string? MachineName { get; set; }
        public string? TechniqueName { get; set; }
        public long? UnitId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public string? Ipdno { get; set; }
        public float? MinValue { get; set; }
        public float? MaxValue { get; set; }
        public long PathReportdetid { get; set; }
    }
}
