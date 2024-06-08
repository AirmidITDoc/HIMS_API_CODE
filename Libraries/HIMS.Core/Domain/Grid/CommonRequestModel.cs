using System.Text.Json.Serialization;

namespace HIMS.Core.Domain.Grid
{
    public class ListRequestModel
    {
        public DateTime? From_Dt { get; set; }
        public DateTime? To_Dt { get; set; }
        public long? Supplier_Id { get; set; }
        public long? ToStoreId { get; set; }
        public string? IsVerify { get; set; }
        public int? Length { get; set; }
        public int? Start { get; set; }
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
    }
    public class CommonFileResponseModel
    {
        public string? htmlFilePath { get; set; }
        public string? htmlHeaderFilePath { get; set; }
        public List<SearchModel>? fields { get; set; }
    }
}