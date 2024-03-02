using System.Collections.Generic;

namespace HIMS.Core.Domain.Grid
{
    public class SearchRequestModel
    {
        public List<SearchGridValue> Filters { get; set; }
        public string Timezone { get; set; }

        public SearchRequestModel()
        {
            Filters = new List<SearchGridValue>();
        }
    }
   
    public class SearchGridValue
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string OpType { get; set; }
    }
}
