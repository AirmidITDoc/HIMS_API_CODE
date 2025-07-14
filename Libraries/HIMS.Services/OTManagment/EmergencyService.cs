using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public class EmergencyService : IEmergencyService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public EmergencyService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<EmergencyListDto>> GetListAsyn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EmergencyListDto>(model, "m_Rtrv_Emergency_list");
        }
        public virtual async Task InsertAsyncSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] Entity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate","City"};
            var entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in Entity)
            {
                entity.Remove(rProperty);
            }
            string VEmgId = odal.ExecuteNonQuery("ps_insert_T_EmergencyAdm_1", CommandType.StoredProcedure, "EmgId", entity);
            objTEmergencyAdm.EmgId = Convert.ToInt32(VEmgId);
        }
        public virtual async Task UpdateSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {
            DatabaseHelper odal = new();
            string[] UEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "City", "AddedBy" };
            var Entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in UEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_EmergencyAdm_1", CommandType.StoredProcedure, Entity);
        }
        public virtual async Task CancelSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {
            DatabaseHelper odal = new();
            string[] CEntity = { "RegId", "EmgDate", "EmgTime", "SeqNo", "FirstName", "MiddleName", "LastName", "Address", "MobileNo", "DepartmentId", "DoctorId", "AddedBy", "UpdatedBy", "IsCancelled", "IsCancelledDate", "PrefixId", "City", "AgeYear", "GenderId","CityId", "IsCancelledBy" };
            var Entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in CEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_Cancel_T_EmergencyAdm_1", CommandType.StoredProcedure, Entity);
        }
    }
}

