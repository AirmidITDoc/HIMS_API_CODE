using System.Text.Json.Serialization;

namespace HIMS.Core.Domain.Grid
{
    public class ListRequestModel
    {
        //public SortingField SortingField { get; set; } = new SortingField();
        //public int PageNumber { get; set; } = 1;
        //public int PageSize { get; set; } = 10;
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();
        public string? mode { get; set; }
      }

    public class ReportRequestModel
    {
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();
        public string? mode { get; set; }
        [JsonIgnore]
        public string baseUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public string storageBaseUrl { get; set; } = string.Empty;
    }

    public class SortingField
    {
        public string FieldName { get; set; }
        public string Direction { get; set; }
    }
    public class SearchFields
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string OpType { get; set; }
    }
    public class SearchModel
    {
        public string? FieldName { get; set; }
        public string? FieldValueString { get; set; }
        public string? FieldOperator { get; set; }
    }

    public class CommonReportModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? BillNo { get; set; }
        public int? DoctorId { get; set; }
        public int? WardId { get; set; }
        public int? AdmissionId { get; set; }
        public string? F_Name { get; set; }
        public string? L_Name { get; set; }
        public string? Reg_No { get; set; }

    }
    public class CommonFileResponseModel
    {
        public string? htmlFilePath { get; set; }
        public string? htmlHeaderFilePath { get; set; }
        public List<SearchModel>? fields { get; set; }
    }
}