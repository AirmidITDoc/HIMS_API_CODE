using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public class CompanyMasterService : ICompanyMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CompanyMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<CompanyMasterListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CompanyMasterListDto>(model, "PS_Rtrv_CompanyMasterList");
        }


        public virtual async Task InsertAsyncsp(List<ServiceWiseCompanyCode> ObjServiceWiseCompanyCode,int UserId, string UserName, long? userId)
        {
            DatabaseHelper odal = new();

            foreach (var item in ObjServiceWiseCompanyCode)
            {
                string[] AEntity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "ServiceDetCompId" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in AEntity)
                {
                    Pentity.Remove(rProperty);
                }
                Pentity["userId"] = userId;
                odal.ExecuteNonQuery("ps_insert_update_ServiceWiseCompany", CommandType.StoredProcedure, Pentity);

            }
        }
        //public virtual async Task InsertAsyncsp(List<ServiceWiseCompanyCode> model, int userId, string username)
        //{
        //    DatabaseHelper odal = new();

        //    foreach (var item in model)
        //    {
        //        string[] AEntity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "ServiceDetCompId" };
        //        var Pentity = item.ToDictionary();

        //        foreach (var rProperty in AEntity)
        //        {
        //            Pentity.Remove(rProperty);
        //        }

        //        Pentity["@userId"] = userId; // <-- add userId manually to parameter dictionary

        //        odal.ExecuteNonQuery("ps_insert_update_ServiceWiseCompany", CommandType.StoredProcedure, Pentity);
        //    }
        //}


    }
}
