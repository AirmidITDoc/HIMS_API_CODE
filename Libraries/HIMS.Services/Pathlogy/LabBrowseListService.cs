using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class LabBrowseListService : ILabBrowseListService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LabBrowseListService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public virtual async Task<IPagedList<LabBrowsListDto>> GetLabListListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabBrowsListDto>(model, "ps_Rtrv_BrowseLABBill_Pagi");
        }

        public virtual async Task<IPagedList<LabBrowsePaymentListDto>> GetLabPaymentListListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabBrowsePaymentListDto>(model, "ps_Rtrv_BrowseOPPaymentList");
        }

        public virtual async Task<IPagedList<LabBrowseRefundListDto>> GetLabRefundListListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabBrowseRefundListDto>(model, "ps_Rtrv_BrowseLABRefundBillList");
        }

    }

}
