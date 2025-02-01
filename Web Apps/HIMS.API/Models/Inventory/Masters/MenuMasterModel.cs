using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class MenuMasterModel
    {
        public int Id { get; set; }
        public int? UpId { get; set; }
        public string LinkName { get; set; } = null!;
        public string? Icon { get; set; }
        public string? LinkAction { get; set; }
        public double? SortOrder { get; set; }
        public bool IsDisplay { get; set; }
        public string? PermissionCode { get; set; }
        public string? TableNames { get; set; }
        public List<PermissionMasterModel>? PermissionMaster { get; set; }

    }
    public class MenuMasterModelValidator : AbstractValidator<MenuMasterModel>
    {
        public MenuMasterModelValidator()
        {
            RuleFor(x => x.UpId).NotNull().NotEmpty().WithMessage("UpId is required");
            RuleFor(x => x.LinkName).NotNull().NotEmpty().WithMessage("LinkName  is required");
            RuleFor(x => x.Icon).NotNull().NotEmpty().WithMessage(" Icon required");


        }
    }
    public class PermissionMasterModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }

    }
    public class PermissionMasterModelValidator : AbstractValidator<PermissionMasterModel>
    {
        public PermissionMasterModelValidator()
        {
            RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("RoleId is required");


        }
    }
}