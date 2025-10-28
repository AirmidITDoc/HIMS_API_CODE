namespace HIMS.Core.Domain.Grid
{
    public class SearchRequestModel
    {
        public List<SearchGridValue> Filters { get; set; }
        public string Timezone { get; set; }

        public SearchRequestModel()
        {
            Filters = new List<SearchGridValue>();
            Timezone = string.Empty; // Initialize Timezone to avoid CS8618
        }
    }

    public class SearchGridValue
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string OpType { get; set; }
    }
}
