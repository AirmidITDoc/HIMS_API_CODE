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
        public MenuService()
        {
        }

        public List<MenuModel> GetMenus(int RoleId, bool isActiveMenuOnly)
        {
            DatabaseHelper sql=new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleId", RoleId);
            List<MenuMasterDTO> lstMenu = sql.FetchListByQuery<MenuMasterDTO>("SELECT M.Id,ISNULL(M.UpId,0) UpId,M.LinkName,M.Icon,M.LinkAction,M.SortOrder,M.IsActive,M.IsDisplay,M.PermissionCode,M.TableNames,P.IsView,P.IsAdd,P.IsEdit,P.IsDelete FROM MenuMaster M LEFT JOIN PermissionMaster P ON M.Id=P.MenuId AND P.RoleId=@RoleId\r\nWHERE IsActive=1 AND IsDisplay=1", para);
            return PrepareMenu(lstMenu, isActiveMenuOnly);
        }
        public static List<MenuModel> PrepareMenu(List<MenuMasterDTO> lstMenu, bool isActiveMenuOnly)
        {
            List<MenuModel> finalList = new();
            try
            {
                var distinct = lstMenu.Where(x => x.UpId == 0);
                foreach (var ItemData in distinct)
                {
                    MenuModel obj = new()
                    {
                        Id = ItemData.Id.ToString(),
                        Icon = ItemData.Icon,
                        Title = ItemData.LinkName,
                        Translate = "",
                        Type = "collapsable",
                        Children = new List<MenuModel>(),
                        IsView = ItemData.IsView,
                        IsAdd = ItemData.IsAdd,
                        IsDelete = ItemData.IsDelete,
                        IsEdit = ItemData.IsEdit
                    };
                    var levelData = lstMenu.Where(x => x.UpId == Convert.ToInt32(obj.Id));
                    foreach (var lData in levelData)
                    {
                        MenuModel test = new()
                        {
                            Id = lData.Id.ToString(),
                            Icon = lData.Icon,
                            Title = lData.LinkName,
                            Translate = "",
                            Type = "collapsable",
                            Children = new List<MenuModel>(),
                            IsView = lData.IsView,
                            IsAdd = lData.IsAdd,
                            IsDelete = lData.IsDelete,
                            IsEdit = lData.IsEdit
                        };
                        test.Children = AddChildtems(lstMenu, test, isActiveMenuOnly);
                        if (test.Children.Count == 0)
                        {
                            test.Type = "item";
                            test.Url = lData.LinkAction;
                            test.Children = null;
                        }
                        if ((test?.Children?.Count ?? 0) > 0 || lData.IsView || !isActiveMenuOnly)
                        {
                            if (test.Children != null)
                            {
                                if (test.Children.Count > 0 && test.Children.Count == test.Children.Count(x => x.IsAdd))
                                    test.IsAdd = true;
                                if (test.Children.Count > 0 && test.Children.Count == test.Children.Count(x => x.IsEdit))
                                    test.IsEdit = true;
                                if (test.Children.Count > 0 && test.Children.Count == test.Children.Count(x => x.IsDelete))
                                    test.IsDelete = true;
                                if (test.Children.Count > 0 && test.Children.Count == test.Children.Count(x => x.IsView))
                                    test.IsView = true;
                            }
                            obj.Children.Add(test);
                        }
                    }
                    if (obj.Children.Count == 0)
                    {
                        obj.Type = "item";
                        obj.Url = ItemData.LinkAction;
                        obj.Children = null;
                    }
                    if ((obj?.Children?.Count ?? 0) > 0 || ItemData.IsView || !isActiveMenuOnly)
                    {
                        if (obj.Children != null)
                        {
                            if (obj.Children.Count > 0 && obj.Children.Count == obj.Children.Count(x => x.IsAdd))
                                obj.IsAdd = true;
                            if (obj.Children.Count > 0 && obj.Children.Count == obj.Children.Count(x => x.IsEdit))
                                obj.IsEdit = true;
                            if (obj.Children.Count > 0 && obj.Children.Count == obj.Children.Count(x => x.IsDelete))
                                obj.IsDelete = true;
                            if (obj.Children.Count > 0 && obj.Children.Count == obj.Children.Count(x => x.IsView))
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
        private static List<MenuModel> AddChildtems(List<MenuMasterDTO> Data, MenuModel obj, bool isActiveMenuOnly)
        {
            List<MenuModel> lstChilds = new();
            try
            {
                //var lstData = Data.Where(x => x.KeyNo.StartsWith(obj.key + "_")).ToList();
                var lstData = Data.Where(x => x.UpId == Convert.ToInt32(obj.Id)).ToList();
                foreach (var objItem in lstData)
                {
                    MenuModel objData = new()
                    {
                        Id = objItem.Id.ToString(),
                        Icon = objItem.Icon,
                        Title = objItem.LinkName,
                        Translate = "",
                        Type = "collapsable",
                        Children = new List<MenuModel>(),
                        IsView = objItem.IsView,
                        IsAdd = objItem.IsAdd,
                        IsDelete = objItem.IsDelete,
                        IsEdit = objItem.IsEdit
                    };
                    objData.Children = AddChildtems(Data, objData, isActiveMenuOnly);
                    if (objData.Children.Count == 0)
                    {
                        objData.Type = "item";
                        objData.Url = objItem.LinkAction;
                        objData.Children = null;
                    }
                    if ((objData?.Children?.Count ?? 0) > 0 || objItem.IsView || !isActiveMenuOnly)
                    {
                        if (objData.Children != null)
                        {
                            if (objData.Children.Count > 0 && objData.Children.Count == objData.Children.Count(x => x.IsAdd))
                                objData.IsAdd = true;
                            if (objData.Children.Count > 0 && objData.Children.Count == objData.Children.Count(x => x.IsEdit))
                                objData.IsEdit = true;
                            if (objData.Children.Count > 0 && objData.Children.Count == objData.Children.Count(x => x.IsDelete))
                                objData.IsDelete = true;
                            if (objData.Children.Count > 0 && objData.Children.Count == objData.Children.Count(x => x.IsView))
                                objData.IsView = true;
                        }
                        lstChilds.Add(objData);
                    }
                }
            }
            catch (Exception)
            {

            }
            return lstChilds;
        }

    }
}
