namespace HIMS.API.Models.Common
{
    public class GridResponseModel
    {
        public object Data { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public int PageIndex { get; set; }
    }
}
