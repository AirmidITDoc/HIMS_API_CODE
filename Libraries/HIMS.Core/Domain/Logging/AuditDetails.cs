namespace HIMS.Core.Domain.Logging
{
    public class AuditDetails 
    {
        public int Id { get; set; }
        public string AuditResponse { get; set; }
        public string AuditChildResponse { get; set; }
    }
}
