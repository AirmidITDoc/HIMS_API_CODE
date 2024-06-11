using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Models
{
    public class PageMasterDto
    {
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public long RoleId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEdit { get; set; }
        public bool IsView { get; set; }
        public int PermissionId { get; set; }
        public bool PageType { get; set; }
    }
}
