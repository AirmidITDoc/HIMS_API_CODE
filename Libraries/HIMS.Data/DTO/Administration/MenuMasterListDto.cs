using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class MenuMasterListDto
    {
        public int Id { get; set; }
        public int UpId { get; set; }
        public string LinkName { get; set; }
        public string Icon { get; set; }
        public string LinkAction { get; set; }
        public double SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisplay { get; set; }
        public string PermissionCode { get; set; }
        public string TableNames { get; set; }
    }
}
