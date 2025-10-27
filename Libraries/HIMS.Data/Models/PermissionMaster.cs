namespace HIMS.Data.Models
{
    public partial class PermissionMaster
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool? IsExport { get; set; }

        public virtual MenuMaster Menu { get; set; } = null!;
    }
}
