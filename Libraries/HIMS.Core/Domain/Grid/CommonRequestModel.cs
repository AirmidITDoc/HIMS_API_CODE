using System.Text.Json.Serialization;

namespace HIMS.Core.Domain.Grid
{
    public class ListRequestModel
    {
        //public SortingField SortingField { get; set; } = new SortingField();
        //public int PageNumber { get; set; } = 1;
        //public int PageSize { get; set; } = 10;
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();
        public string? Mode { get; set; }
      }

    public class ReportRequestModel
    {
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();
        public string? Mode { get; set; }
        [JsonIgnore]
        public string BaseUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public string StorageBaseUrl { get; set; } = string.Empty;
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
    public class CommonFileResponseModel
    {
        public string? HtmlFilePath { get; set; }
        public string? HtmlHeaderFilePath { get; set; }
        public List<SearchModel>? Fields { get; set; }
    }
}