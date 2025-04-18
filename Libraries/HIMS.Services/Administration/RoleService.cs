using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.User;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public class RoleService : IRoleService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public RoleService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public List<MenuModel> PrepareMenu(List<MenuMasterDTO> lstMenu, bool isActiveMenuOnly)
        {
            List<MenuModel> finalList = new();
            try
            {
                var distinct = lstMenu.Where(x => x.UpId == 0 || x.UpId == null);
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
                        IsEdit = ItemData.IsEdit,
                        IsExport = ItemData.IsExport
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
                            IsEdit = lData.IsEdit,
                            IsExport = lData.IsExport
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
                                if (test.Children.Count > 0 && test.Children.Count == test.Children.Count(x => x.IsExport))
                                    test.IsExport = true;
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
                            if (obj.Children.Count > 0 && obj.Children.Count == obj.Children.Count(x => x.IsExport))
                                obj.IsExport = true;
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
        private List<MenuModel> AddChildtems(List<MenuMasterDTO> Data, MenuModel obj, bool isActiveMenuOnly)
        {
            List<MenuModel> lstChilds = new List<MenuModel>();
            try
            {
                //var lstData = Data.Where(x => x.KeyNo.StartsWith(obj.key + "_")).ToList();
                var lstData = Data.Where(x => x.UpId == Convert.ToInt32(obj.Id)).ToList();
                foreach (var objItem in lstData)
                {
                    MenuModel objData = new MenuModel()
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
                            if (objData.Children.Count > 0 && objData.Children.Count == objData.Children.Count(x => x.IsExport))
                                objData.IsExport = true;
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
        public List<MenuModel> GetPermisison(int RoleId)
        {
            // return GetListBySp<MenuMaster>("SELECT M.* FROM MenuMaster M LEFT JOIN MenuMaster MM on M.Id=MM.UpId WHERE ISNULL(MM.Id,0)=0", new SqlParameter[0]);
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleId", RoleId);
            List<MenuMasterDTO> lstMenu = sql.FetchListBySP<MenuMasterDTO>("GET_PERMISSION", para);
            return PrepareMenu(lstMenu, false);
        }
        public void SavePermission(List<PermissionModel> lst)
        {
            DataTable dt = new();
            dt.Columns.Add("RoleID", typeof(int));
            dt.Columns.Add("MenuId", typeof(int));
            dt.Columns.Add("IsView", typeof(int));
            dt.Columns.Add("IsAdd", typeof(int));
            dt.Columns.Add("IsEdit", typeof(int));
            dt.Columns.Add("IsDelete", typeof(int));
            dt.Columns.Add("IsExport", typeof(int));
            foreach (var item in lst)
                dt.Rows.Add(item.RoleId, item.MenuId, item.IsView, item.IsAdd, item.IsEdit, item.IsDelete);
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@tbl", SqlDbType.Structured)
            {
                Value = dt,
                TypeName = "dbo.Permission"
            };
            sql.ExecuteNonQuery("Insert_Permission", CommandType.StoredProcedure, para);
        }

    }
}
