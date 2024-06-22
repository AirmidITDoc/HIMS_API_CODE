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
    public partial class MenuMaster
    {
        public int RoleId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
    public class MenuModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string translate { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public List<MenuModel> children { get; set; }
    }
    public class FavouriteModel
    {
        public Int64 UserId { get; set; }
        public string LinkName { get; set; }
        public int MenuId { get; set; }
        public string LinkAction { get; set; }
        public string Icon { get; set; }
        public bool IsFavourite { get; set; }
    }
}
