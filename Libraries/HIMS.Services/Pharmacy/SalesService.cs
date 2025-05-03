using HIMS.Core.Domain.Grid;
using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Master;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public class SalesService : ISalesService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SalesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TSalesHeader objSales, Payment objPayment, int UserId, string Username)
        {
            objSales.RoundOff = objSales.PaidAmount - objSales.NetAmount;
            _context.TSalesHeaders.Add(objSales);
            await _context.SaveChangesAsync();
            DatabaseHelper odal = new();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@SalesId", objSales.SalesId);
            para[1] = new SqlParameter("@StoreId", objSales.StoreId);
            odal.ExecuteScalar("UpdateBillNo", CommandType.StoredProcedure, para);
            foreach (var objItem in objSales.TSalesDetails)
            {
                TCurrentStock objStock = await _context.TCurrentStocks.FirstOrDefaultAsync(x => x.StockId == objItem.StkId && x.StoreId == objSales.StoreId && x.ItemId == objItem.ItemId);
                if (objStock != null)
                {
                    objStock.IssueQty += (float)objItem.Qty;
                    objStock.BalanceQty += (float)objItem.Qty;
                    _context.Entry(objStock).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            var config = (await _context.ConfigSettings.ToListAsync()).FirstOrDefault();
            var StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objSales.StoreId);
            if (config != null)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    transaction.CreateSavepoint("insert_Payment_Pharmacy_New_1");
                    if (objSales.OpIpType == 0 || objSales.OpIpType == 1 || objSales.OpIpType == 3)
                    {
                        objPayment.CashCounterId = objSales.OpIpType == 0 ? config.OpdReceiptCounterId : (objSales.OpIpType == 1 ? config.IpdReceiptCounterId : StoreInfo.PharSalRecCountId);
                        var objCashCounter = await _context.CashCounters.FirstOrDefaultAsync(x => x.CashCounterId == objPayment.CashCounterId);
                        objPayment.ReceiptNo = (objCashCounter != null ? Convert.ToInt64(objCashCounter.BillNo) + 1 : 1).ToString();
                        objPayment.BillNo = objSales.SalesId;
                        _context.Payments.Add(objPayment);
                        objCashCounter.BillNo = objPayment.ReceiptNo;
                        _context.Entry(objCashCounter).State = EntityState.Modified;
                        await _context.SaveChangesAsync(UserId, Username);
                    }
                    else if (objSales.OpIpType == 4)
                    {
                        PaymentPharmacy objPharmacy = await _context.PaymentPharmacies.FirstOrDefaultAsync(x => x.BillNo == objSales.SalesId);
                        if ((objPharmacy?.PaymentId ?? 0) > 0)
                        {
                            objPharmacy.StrId = objSales.StoreId;
                            _context.Entry(objPharmacy).State = EntityState.Modified;
                            await _context.SaveChangesAsync(UserId, Username);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.RollbackToSavepoint("insert_Payment_Pharmacy_New_1");
                }
            }

            if (objSales.OpIpType == 0)
            {

            }
        }


        public virtual async Task<IPagedList<PharSalesListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharSalesListDto>(model, "m_PHAR_SalesList");
        }
        public virtual async Task<IPagedList<PharSalesCurrentSumryListDto>> GetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharSalesCurrentSumryListDto>(model, "m_rtrv_Phar_SalesList_CurrentSumry");
        }
        public virtual async Task<IPagedList<PharCurrentDetListDto>> SalesDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharCurrentDetListDto>(model, "m_rtrv_Phar_SalesList_CurrentDet");
        }
        public virtual async Task<IPagedList<SalesRetrunCurrentSumryListDto>> SalesReturnSummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesRetrunCurrentSumryListDto>(model, "m_rtrv_Phar_SalesRetrunList_CurrentSumry");
        }
        public virtual async Task<IPagedList<SalesRetrunLCurrentDetListDto>> SalesReturnDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesRetrunLCurrentDetListDto>(model, "m_rtrv_Phar_SalesRetrunList_CurrentDet");
        }
        public virtual async Task<IPagedList<SalesDetailsListDto>> Getsalesdetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesDetailsListDto>(model, "Rtrv_SalesDetails");
        }

        public virtual async Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesBillListDto>(model, "Rtrv_SalesBillList");
        }
        public virtual async Task<IPagedList<SalesReturnDetailsListDto>> salesreturndetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnDetailsListDto>(model, "Rtrv_SalesReturnDetails");
        }
        public virtual async Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnBillListDto>(model, "Rtrv_SalesReturnBillList");
        }
    }
}
