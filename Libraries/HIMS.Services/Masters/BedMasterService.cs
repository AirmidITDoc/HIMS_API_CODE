using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Master;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public class BedMasterService : IBedMasterService
    {
        private readonly HIMSDbContext _context;
        public BedMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task UpdateAsync(Bedmaster ObjBedmaster, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "" };
            var Rentity = ObjBedmaster.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Reset_BedMaster", CommandType.StoredProcedure, Rentity);
            //await _context.LogProcedureExecution(Rentity, nameof(Bedmaster), ObjBedmaster.BedId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }


        public virtual async Task<IPagedList<BedmasterListDto>> GetBedListListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BedmasterListDto>(model, "ps_rtrv_GetBedMasterList");
        }
    }
}
