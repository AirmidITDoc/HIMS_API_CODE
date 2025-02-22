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
        public bool? IsAdd { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsView { get; set; }
        public bool PageType { get; set; }
    }
    public class MenuMasterDTO : MenuMaster
    {
        public int RoleId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
    public class MenuModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Translate { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public List<MenuModel> Children { get; set; }
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
