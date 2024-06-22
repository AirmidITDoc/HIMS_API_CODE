using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public class MenuService : IMenuService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MenuService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public List<MenuModel> GetMenus(int RoleId, bool isActiveMenuOnly)
        {
            DatabaseHelper sql=new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleId", RoleId);
            List<MenuMaster> lstMenu = sql.FetchListByQuery<MenuMaster>("SELECT M.Id,ISNULL(M.UpId,0) UpId,M.LinkName,M.Icon,M.LinkAction,M.SortOrder,M.IsActive,M.IsDisplay,M.PermissionCode,M.TableNames,P.IsView,P.IsAdd,P.IsEdit,P.IsDelete FROM MenuMaster M LEFT JOIN PermissionMaster P ON M.Id=P.MenuId AND P.RoleId=@RoleId\r\nWHERE IsActive=1 AND IsDisplay=1", para);
            return PrepareMenu(lstMenu, isActiveMenuOnly);
        }
        public List<MenuModel> PrepareMenu(List<MenuMaster> lstMenu, bool isActiveMenuOnly)
        {
            List<MenuModel> finalList = new();
            try
            {
                var distinct = lstMenu.Where(x => x.UpId == 0);
                foreach (var ItemData in distinct)
                {
                    MenuModel obj = new()
                    {
                        id = ItemData.Id.ToString(),
                        icon = ItemData.Icon,
                        title = ItemData.LinkName,
                        translate = "",
                        type = "collapsable",
                        children = new List<MenuModel>(),
                        IsView = ItemData.IsView,
                        IsAdd = ItemData.IsAdd,
                        IsDelete = ItemData.IsDelete,
                        IsEdit = ItemData.IsEdit
                    };
                    var levelData = lstMenu.Where(x => x.UpId == Convert.ToInt32(obj.id));
                    foreach (var lData in levelData)
                    {
                        MenuModel test = new()
                        {
                            id = lData.Id.ToString(),
                            icon = lData.Icon,
                            title = lData.LinkName,
                            translate = "",
                            type = "collapsable",
                            children = new List<MenuModel>(),
                            IsView = lData.IsView,
                            IsAdd = lData.IsAdd,
                            IsDelete = lData.IsDelete,
                            IsEdit = lData.IsEdit
                        };
                        test.children = AddChildtems(lstMenu, test, isActiveMenuOnly);
                        if (test.children.Count == 0)
                        {
                            test.type = "item";
                            test.url = lData.LinkAction;
                            test.children = null;
                        }
                        if ((test?.children?.Count ?? 0) > 0 || lData.IsView || !isActiveMenuOnly)
                        {
                            if (test.children != null)
                            {
                                if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsAdd))
                                    test.IsAdd = true;
                                if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsEdit))
                                    test.IsEdit = true;
                                if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsDelete))
                                    test.IsDelete = true;
                                if (test.children.Count > 0 && test.children.Count == test.children.Count(x => x.IsView))
                                    test.IsView = true;
                            }
                            obj.children.Add(test);
                        }
                    }
                    if (obj.children.Count == 0)
                    {
                        obj.type = "item";
                        obj.url = ItemData.LinkAction;
                        obj.children = null;
                    }
                    if ((obj?.children?.Count ?? 0) > 0 || ItemData.IsView || !isActiveMenuOnly)
                    {
                        if (obj.children != null)
                        {
                            if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsAdd))
                                obj.IsAdd = true;
                            if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsEdit))
                                obj.IsEdit = true;
                            if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsDelete))
                                obj.IsDelete = true;
                            if (obj.children.Count > 0 && obj.children.Count == obj.children.Count(x => x.IsView))
                                obj.IsView = true;
                        }
                        finalList.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return finalList;
        }
        private static List<MenuModel> AddChildtems(List<MenuMaster> Data, MenuModel obj, bool isActiveMenuOnly)
        {
            List<MenuModel> lstChilds = new();
            try
            {
                //var lstData = Data.Where(x => x.KeyNo.StartsWith(obj.key + "_")).ToList();
                var lstData = Data.Where(x => x.UpId == Convert.ToInt32(obj.id)).ToList();
                foreach (var objItem in lstData)
                {
                    MenuModel objData = new()
                    {
                        id = objItem.Id.ToString(),
                        icon = objItem.Icon,
                        title = objItem.LinkName,
                        translate = "",
                        type = "collapsable",
                        children = new List<MenuModel>(),
                        IsView = objItem.IsView,
                        IsAdd = objItem.IsAdd,
                        IsDelete = objItem.IsDelete,
                        IsEdit = objItem.IsEdit
                    };
                    objData.children = AddChildtems(Data, objData, isActiveMenuOnly);
                    if (objData.children.Count == 0)
                    {
                        objData.type = "item";
                        objData.url = objItem.LinkAction;
                        objData.children = null;
                    }
                    if ((objData?.children?.Count ?? 0) > 0 || objItem.IsView || !isActiveMenuOnly)
                    {
                        if (objData.children != null)
                        {
                            if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsAdd))
                                objData.IsAdd = true;
                            if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsEdit))
                                objData.IsEdit = true;
                            if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsDelete))
                                objData.IsDelete = true;
                            if (objData.children.Count > 0 && objData.children.Count == objData.children.Count(x => x.IsView))
                                objData.IsView = true;
                        }
                        lstChilds.Add(objData);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstChilds;
        }

    }
}
