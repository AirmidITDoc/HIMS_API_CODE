namespace HIMS.API.Models.DocumentManagement
{
    public class DocumentCategoryModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string DocCategory { get; set; } = null!;
        public string? Icon { get; set; }
        public int? SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
