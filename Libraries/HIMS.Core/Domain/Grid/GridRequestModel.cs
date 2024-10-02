using System.Collections.Generic;
using System.Linq.Expressions;

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
        //public List<GridColumn> Columns { get; set; }
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
        public OperatorComparer OpType { get; set; }
    }
    public enum ExportType
    {
        JSON=1,Excel = 2, Pdf = 3
    }
    public enum OperatorComparer
    {
        Contains,
        StartsWith,
        EndsWith,
        Equals = ExpressionType.Equal,
        GreaterThan = ExpressionType.GreaterThan,
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,
        LessThan = ExpressionType.LessThan,
        LessThanOrEqual = ExpressionType.LessThanOrEqual,
        NotEqual = ExpressionType.NotEqual,
        InClause,
        DateRange
    }
}
