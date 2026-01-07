using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public  class InPatientService : IInPatientService
    {

        private readonly Data.Models.HIMSDbContext _context;
        public InPatientService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesBillListDto>(model, "ps_Rtrv_SalesInPatientBillList");
        }
        public virtual async Task<IPagedList<InPatientSalesDetailsListDto>> Getsalesdetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<InPatientSalesDetailsListDto>(model, "ps_Rtrv_SalesInPatientDetails");
        }
        public virtual async Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnBillListDto>(model, "ps_Rtrv_SalesInPatientReturnBillList");
        }
        public virtual async Task<IPagedList<SalesInPatientReturnDetailsListDto>> salesreturndetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesInPatientReturnDetailsListDto>(model, "ps_Rtrv_InPatientSalesReturnDetails");
        }
    }
}
