using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using HIMS.Services.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using static LinqToDB.Common.Configuration;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.IPPatient
{
    public class AdvanceService : IAdvanceService
    {
        private readonly HIMSDbContext _context;
        public AdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<IPRefundAdvanceReceiptListDto>> IPRefundAdvanceReceiptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPRefundAdvanceReceiptListDto>(model, "Retrieve_BrowseIPRefundAdvanceReceipt");
        }
        public virtual async Task<IPagedList<IPAdvanceListDto>> IPAdvanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPAdvanceListDto>(model, "m_Rtrv_BrowseIPAdvanceList");
        }
        public virtual async Task<IPagedList<AdvanceDetailListDto>> AdvanceDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdvanceDetailListDto>(model, "Rtrv_T_AdvanceList");
        }

        public virtual async Task<IPagedList<AdvanceListDto>> GetAdvanceListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdvanceListDto>(model, "m_Rtrv_BrowseIPAdvanceList");
        }
        public virtual async Task<IPagedList<RefundOfAdvanceListDto>> GetRefundOfAdvanceListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RefundOfAdvanceListDto>(model, "m_Rtrv_BrowseIPRefundAdvanceReceipt");
        }

        //SHILPA CODE////
        public virtual async Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "AdvanceDetails" };
            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vAdvanceId = odal.ExecuteNonQuery("sp_insert_AdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(vAdvanceId);
            objAdvanceDetail.AdvanceId = Convert.ToInt32(vAdvanceId);


            string[] rDetailEntity = { "AdvanceNo", "Advance" };

            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            string AdvanceDetailId = odal.ExecuteNonQuery("sp_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AdvanceEntity);
            objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);


            string[] PayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity)
            {
                PAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_m_insert_Payment_1", CommandType.StoredProcedure, PAdvanceEntity);


        }
        //SHILPA CODE////
        public virtual async Task UpdateAdvanceSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceUsedAmount", "BalanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };
            var AdvanceEntity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_Update_AdvHeader_1", CommandType.StoredProcedure, AdvanceEntity);

            string[] DetailEntity = { "AdvanceNo", "Advance" };
            var AAdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in DetailEntity)
            {
                AAdvanceEntity.Remove(rProperty);
            }
            string AdvanceDetailId = odal.ExecuteNonQuery("sp_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AAdvanceEntity);
            objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);


            string[] PayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity)
            {
                PAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_m_insert_Payment_1", CommandType.StoredProcedure, PAdvanceEntity);

        }
    }
       
}





