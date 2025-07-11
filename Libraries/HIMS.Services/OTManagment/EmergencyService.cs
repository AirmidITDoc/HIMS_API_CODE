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
        public virtual async Task<IPagedList<EmergencyListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EmergencyListDto>(model, "m_Rtrv_Emergency_list");
        }
        public virtual async Task InsertAsyncSP(TEmergencyAdm objTEmergencyAdm, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "PrefixId", "City", "AgeYear", "GenderId" };
            var entity = objTEmergencyAdm.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VEmgId = odal.ExecuteNonQuery("ps_insert_T_EmergencyAdm_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objTEmergencyAdm.EmgId = Convert.ToInt32(VEmgId);
        }
    }
}

