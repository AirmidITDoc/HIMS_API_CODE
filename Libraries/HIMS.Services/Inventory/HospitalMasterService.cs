using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public  class HospitalMasterService:IHospitalMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HospitalMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<HospitalMasterListDto>> GetListAsyncH(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<HospitalMasterListDto>(model, "ps_Rtrv_HospitalMaster_Pagn");
        }

    }
}
