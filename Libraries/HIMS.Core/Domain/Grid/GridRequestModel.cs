using System.Collections.Generic;

namespace HIMS.Core.Domain.Grid
{
    public class GridRequestModel
    {
        public int First { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
        public List<SearchGrid> Filters { get; set; }
        public ExportType ExportType { get; set; }
        //public string GlobalFilterText { get; set; }
        //public string GlobalFilterColumns { get; set; }
        public List<GridColumn> Columns { get; set; }
    }
    public class GridColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
    }
    public class SearchGrid
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string OpType { get; set; }
    }
    public enum ExportType
    {
        Excel = 1, Pdf = 2
    }
}
