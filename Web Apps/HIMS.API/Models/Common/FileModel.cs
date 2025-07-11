namespace HIMS.API.Models.Common
{
    public class FileModel
    {
        public long Id { get; set; }
        public long? RefId { get; set; }
        public long? RefType { get; set; }
        public IFormFile Document { get; set; } 
        public string? DocName { get; set; }
        public string? DocSavedName { get; set; }
        public bool? IsDelete { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
