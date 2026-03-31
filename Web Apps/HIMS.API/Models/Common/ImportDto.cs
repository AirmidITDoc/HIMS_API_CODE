namespace HIMS.API.Models.Common
{
    public class ImportDto
    {
        public string targetColumn { get; set; } = "";
        public string sourceColumn { get; set; } = "";
    }

    public class ImportBaseDto
    {
        public int Status { get; set; }
        public string Message { get; set; } = "";
    }
}
