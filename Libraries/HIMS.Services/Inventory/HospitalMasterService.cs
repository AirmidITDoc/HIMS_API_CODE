using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public class HospitalMasterService : IHospitalMasterService
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
