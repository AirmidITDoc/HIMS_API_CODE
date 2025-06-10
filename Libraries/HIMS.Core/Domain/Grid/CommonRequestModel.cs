using System.Text.Json.Serialization;

namespace HIMS.Core.Domain.Grid
{
    public class ListRequestModel
    {
        //public SortingField SortingField { get; set; } = new SortingField();
        //public int PageNumber { get; set; } = 1;
        //public int PageSize { get; set; } = 10;
        public List<SearchGrid> SearchFields { get; set; } = new List<SearchGrid>();
        public string? Mode { get; set; }
    }

    public class ReportRequestModel
    {
        public List<SearchGrid> SearchFields { get; set; } = new List<SearchGrid>();
        public string? Mode { get; set; }
        [JsonIgnore]
        public string BaseUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public string StorageBaseUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public string RepoertName { get; set; } = string.Empty;
    }

    public class ReportNewRequestModel
    {
        public List<SearchGrid> SearchFields { get; set; } = new List<SearchGrid>();
        public string? Mode { get; set; }
        [JsonIgnore]
        public string BaseUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public string StorageBaseUrl { get; set; } = string.Empty;
        public string RepoertName { get; set; } = string.Empty;
        public string[] headerList { get; set; } = null;
        public string[] colList { get; set; } = null;
        public string[] totalFieldList { get; set; } = null;
        //public string[] groupbyList { get; set; } = null;
        public string groupByLabel { get; set; } = null;
        public string summaryLabel { get; set; } = null;
        public string htmlFilePath { get; set; } = string.Empty;
        public string htmlHeaderFilePath { get; set; } = string.Empty;
        public string SPName { get; set; } = string.Empty;
        public string FolderName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string vPageOrientation { get; set; }

    }

    public class SortingField
    {
        public string FieldName { get; set; }
        public string Direction { get; set; }
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

    public class DDLRequestModel
    {
        public string? Mode { get; set; }
        public int Id { get; set; } = 0;
    }
}