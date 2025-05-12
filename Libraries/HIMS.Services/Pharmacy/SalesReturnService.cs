using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public class SalesReturnService : ISalesReturnService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SalesReturnService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SalesReturnDetailsListDto>> salesreturndetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnDetailsListDto>(model, "ps_Rtrv_SalesReturnDetails");
        }
        public virtual async Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnBillListDto>(model, "ps_Rtrv_SalesReturnBillList");
        }
        public virtual async Task<IPagedList<SalesRetrunCurrentSumryListDto>> SalesReturnSummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesRetrunCurrentSumryListDto>(model, "m_rtrv_Phar_SalesRetrunList_CurrentSumry");
        }
        public virtual async Task<IPagedList<SalesRetrunLCurrentDetListDto>> SalesReturnDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesRetrunLCurrentDetListDto>(model, "m_rtrv_Phar_SalesRetrunList_CurrentDet");
        }
    }
}
