using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Radiology
{
    public class RadiologyService : IRadilogyService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public RadiologyService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<RadiologyListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RadiologyListDto>(model, "m_Rtrv_RadilogyResultEntryList_Ptnt_Dtls");
        }
        public virtual async Task RadiologyUpdate(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string UserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] REntity = { "RadDate", "RadTime", "OpdIpdType", "OpdIpdId", "RadTestId", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "TestType" };
            var Dentity = ObjTRadiologyReportHeader.ToDictionary();
            foreach (var rProperty in REntity)
            {
                Dentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_T_RadiologyReportHeader_1", CommandType.StoredProcedure, Dentity);
        }
    }
}
