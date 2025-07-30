using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace HIMS.Services.Nursing
{
    public partial class NursingConsentService : INursingConsentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public NursingConsentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ConsentDeptListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ConsentDeptListDto>(model, "m_rtrv_ConsentMasterList");
        }


        public virtual async Task<List<MConsentMaster>> GetConsent(int DeptId)
        {
            var qry = from s in _context.MConsentMasters
              .Where(x => (x.DepartmentId == DeptId))
                      select new MConsentMaster()
                      {
                          ConsentId = s.ConsentId,
                          ConsentName = s.ConsentName,
                          ConsentDesc = s.ConsentDesc,

                      };
            return await qry.Take(50).ToListAsync();



        }



        public virtual async Task InsertAsync(TConsentInformation ObjTConsentInformation, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "CreatedDatetime", "ModifiedBy",  "ModifiedDateTime"};
            var entity = ObjTConsentInformation.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            string AConsentId = odal.ExecuteNonQuery("m_insert_T_ConsentInformation", CommandType.StoredProcedure, "ConsentId", entity);
            ObjTConsentInformation.ConsentId = Convert.ToInt32(AConsentId);

        }

        public virtual async Task UpdateAsync(TConsentInformation ObjTConsentInformation, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "CreatedDatetime", "CreatedBy", "ModifiedDateTime" };
            var entity = ObjTConsentInformation.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

             odal.ExecuteNonQuery("m_update_T_ConsentInformation", CommandType.StoredProcedure, entity);
            

        }

        public virtual async Task<IPagedList<ConsentpatientInfoListDto>> ConsentpatientInfoList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ConsentpatientInfoListDto>(model, "m_rtrv_ConsentpatientInformation_List");
        }
    }
}