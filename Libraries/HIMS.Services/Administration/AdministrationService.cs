using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public  class AdministrationService: IAdministrationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AdministrationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        
        public virtual async Task<IPagedList<BrowseOPDBillPagiListDto>> BrowseOPDBillPagiList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseOPDBillPagiListDto>(model, "m_Rtrv_BrowseOPDBill_Pagi");
        }
        public virtual async Task<IPagedList<IPAdvanceListDto>> IPAdvanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPAdvanceListDto>(model, "m_Rtrv_BrowseIPAdvanceList");
        }
        public virtual async Task<IPagedList<IPRefundAdvanceReceiptListDto>> IPRefundAdvanceReceiptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPRefundAdvanceReceiptListDto>(model, "Retrieve_BrowseIPRefundAdvanceReceipt");
        }
        public virtual async Task<IPagedList<RoleMasterListDto>> RoleMasterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RoleMasterListDto>(model, "m_Rtrv_Rolemaster");
        }

        public virtual async Task<IPagedList<PaymentModeDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PaymentModeDto>(model, "Retrieve_BrowseIPAdvPaymentReceipt");
        }
        public virtual async Task<IPagedList<BrowseIPAdvPayPharReceiptListDto>> BrowseIPAdvPayPharReceiptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPAdvPayPharReceiptListDto>(model, "Retrieve_BrowseIPAdvPayPharReceipt");
        }

    }
}
