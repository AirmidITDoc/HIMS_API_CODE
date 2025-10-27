namespace HIMS.Data.DTO.Administration
{
    public class RoleMasterListDto
    {
        public long RoleId { get; set; }
        public string? RoleName { get; set; }

        public bool? IsActive { get; set; }
    }
}
