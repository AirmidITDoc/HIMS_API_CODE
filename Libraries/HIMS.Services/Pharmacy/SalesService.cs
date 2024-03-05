using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
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
        public virtual async Task InsertAsync(TSalesHeader objSales, int UserId, string Username)
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
                    Payment objPayment = new();
                    if (objSales.OpIpType == 0 || objSales.OpIpType == 1 || objSales.OpIpType == 3)
                    {
                        objPayment.CashCounterId = objSales.OpIpType == 0 ? config.OpdReceiptCounterId : (objSales.OpIpType == 1 ? config.IpdReceiptCounterId : StoreInfo.PharSalRecCountId);
                        var objCashCounter = await _context.CashCounters.FirstOrDefaultAsync(x => x.CashCounterId == objPayment.CashCounterId);
                        objPayment.ReceiptNo = (objCashCounter != null ? Convert.ToInt64(objCashCounter.BillNo) + 1 : 1).ToString();


                        _context.Payments.Add(objPayment);
                        objCashCounter.BillNo = objPayment.ReceiptNo;
                        _context.Entry(objCashCounter).State = EntityState.Modified;
                        await _context.SaveChangesAsync(UserId, Username);
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
    }
}
