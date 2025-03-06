using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using System.Threading.Tasks;
using System.Transactions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;

namespace HIMS.Services.OPPatient
{
    public class OPRefundOfBillService : IOPRefundOfBillService
    {
        private readonly HIMSDbContext _context;
        public OPRefundOfBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<OpBilllistforRefundDto>> GeOpbilllistforrefundAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OpBilllistforRefundDto>(model, "m_OPBillListforRefund");
        }

        public virtual async Task<IPagedList<OPBillservicedetailListDto>> GetBillservicedetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillservicedetailListDto>(model, "m_rtrv_OPBill_For_Refund");
        }

        public virtual async Task<IPagedList<RefundAgainstBillListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RefundAgainstBillListDto>(model, "m_rtrv_OPDRefundAgainstBillList");
        }

        public virtual async Task<IPagedList<IPBillListforRefundListDto>> IPBillGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillListforRefundListDto>(model, "m_IPBillListforRefund");
        }



        public virtual async Task InsertAsyncOP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };

            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vRefundId= odal.ExecuteNonQuery("ps_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(vRefundId);
            objPayment.RefundId = Convert.ToInt32(vRefundId);

            foreach (var item in objTRefundDetail)
            {
                item.RefundId = Convert.ToInt32(vRefundId);
                string[] rRefundEntity = { "HospitalAmount", "DoctorAmount", "RefundDetId", "UpdatedBy", "Refund" };
                var RefundEntity = item.ToDictionary();
                foreach (var rProperty in rRefundEntity)
                {
                    RefundEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_RefundDetails_1", CommandType.StoredProcedure, RefundEntity);
            }


            foreach (var item in objAddCharge)
            {
                string[] rChargeEntity = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "PackageMainChargeId", "ClassId", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo" , "IsHospMrk", "BillNoNavigation" };
                var ChargeEntity = item.ToDictionary();
                foreach (var rProperty in rChargeEntity)
                 {
                   ChargeEntity.Remove(rProperty);
                 }
                odal.ExecuteNonQuery("ps_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);
            }

            string[] rPayEntity = { "PaymentId","CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode"};
            var PayEntity = objPayment.ToDictionary();
            foreach (var rProperty in rPayEntity)
            {
                PayEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, PayEntity);
        }

        //public virtual async Task InsertAsyncOP(Refund objRefund, int CurrentUserId, string CurrentUserName)
        //{
        //    try
        //    {
        //        // Bill Code
        //        DatabaseHelper odal = new();
        //        string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };
        //        var entity = objRefund.ToDictionary();
        //        foreach (var rProperty in rEntity)
        //        {
        //            entity.Remove(rProperty);
        //        }
        //        string RefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
        //        objRefund.RefundId = Convert.ToInt32(RefundId);

        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            foreach (var objAddCharge in objRefund.AddCharges)
        //            {
        //                // Add Charges Code
        //                //objAddCharge.ChargesId = objRefund.ChargesId;
        //                objAddCharge.RefundAmount = objRefund.RefundAmount;
        //                //objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
        //                //objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
        //                //objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
        //                _context.AddCharges.Add(objAddCharge);
        //                await _context.SaveChangesAsync();

        //                // TRefundDetail  Code
        //                foreach (var objTRefundDetail in objRefund.TRefundDetails)
        //                {
        //                    objTRefundDetail.RefundId = objRefund.RefundId;
        //                    objTRefundDetail.ServiceId = objTRefundDetail?.ServiceId;
        //                    objTRefundDetail.ServiceAmount = objTRefundDetail?.ServiceAmount;
        //                    objTRefundDetail.RefundAmount = objRefund.RefundAmount;
        //                    objTRefundDetail.DoctorId = objTRefundDetail?.DoctorId;
        //                    objTRefundDetail.Remark = objRefund.Remark;
        //                    objTRefundDetail.AddBy = objRefund.AddedBy;
        //                    objTRefundDetail.ChargesId = objTRefundDetail?.ChargesId;

        //                    _context.TRefundDetails.Add(objTRefundDetail);
        //                    await _context.SaveChangesAsync();
        //                }

        //                // TRefundDetail Code
        //                //if (objRefund?.RefundId == 1)
        //                //{
        //                //    TRefundDetail objTRefundDetail = new TRefundDetail();
        //                //    objTRefundDetail.RefundId = objRefund.RefundId;
        //                //    //objRefund.ServiceId = objTRefundDetail?.ServiceId;
        //                //    //objRefund.OpdIpdType = objTRefundDetail?.ServiceAmount;
        //                //    objTRefundDetail.RefundAmount = objRefund?.RefundAmount;
        //                //    //objRefund.RadTestId = objTRefundDetail?.DoctorId;
        //                //    objTRefundDetail.Remark = objRefund?.Remark;
        //                //    //objTRefundDetail.AddedBy = objRefund?.AddBy;
        //                //    //objRefund.ChargeId = objTRefundDetail?.ChargesId;
        //                //    //objRefund.IsCompleted = false;
        //                //    //objRefund.IsCancelled = 0;
        //                //    //objRefund.IsPrinted = false;
        //                //    //objRefund.TestType = false;

        //                //    _context.TRefundDetails.Add(objTRefundDetail);
        //                //    await _context.SaveChangesAsync();
        //                //}
        //            }

        //            // Payment Code
        //            int _val = 0;
        //            foreach (var objPayment in objRefund.Payments)
        //            {
        //                if (_val == 0)
        //                {
        //                    objPayment.PaymentId = objPayment.PaymentId;
        //                    objPayment.BillNo = objRefund.BillId;
        //                    objPayment.ReceiptNo = objPayment?.ReceiptNo;
        //                    objPayment.PaymentDate = objPayment?.PaymentDate;
        //                    objPayment.PaymentTime = objPayment?.PaymentTime;
        //                    objPayment.CashPayAmount = objPayment?.CashPayAmount;
        //                    objPayment.ChequePayAmount = objPayment?.ChequePayAmount;
        //                    objPayment.ChequeNo = objPayment?.ChequeNo;
        //                    objPayment.BankName = objPayment?.BankName;
        //                    objPayment.ChequeDate = objPayment?.ChequeDate;
        //                    objPayment.CardPayAmount = objPayment?.CardPayAmount;
        //                    objPayment.CardNo = objPayment?.CardNo;
        //                    objPayment.CardBankName = objPayment?.CardBankName;
        //                    objPayment.CardDate = objPayment?.CardDate;
        //                    objPayment.AdvanceUsedAmount = objPayment?.AdvanceUsedAmount;
        //                    objPayment.AdvanceId = objRefund.AdvanceId;
        //                    objPayment.RefundId = objRefund.RefundId;
        //                    objPayment.TransactionType = objPayment?.TransactionType;
        //                    objPayment.Remark = objRefund.Remark;
        //                    objPayment.AddBy = objRefund.AddedBy;
        //                    objPayment.IsCancelled = objRefund.IsCancelled;
        //                    objPayment.NeftpayAmount = objPayment?.NeftpayAmount;
        //                    objPayment.Neftno = objPayment?.Neftno;
        //                    objPayment.NeftbankMaster = objPayment?.NeftbankMaster;
        //                    objPayment.Neftdate = objPayment?.Neftdate;
        //                    objPayment.PayTmamount = objPayment?.PayTmamount;
        //                    objPayment.PayTmtranNo = objPayment?.PayTmtranNo;
        //                    objPayment.PayTmdate = objPayment?.PayTmdate;



        //                    objPayment.BillNo = objRefund.BillId;
        //                    _context.Payments.Add(objPayment);
        //                    await _context.SaveChangesAsync();
        //                }
        //                _val += 1;
        //            }
        //            scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Refund? objRefunds = await _context.Refunds.FindAsync(objRefund.RefundId);
        //        _context.Refunds.Remove(objRefunds);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public virtual async Task InsertAsyncIP(Refund objRefund, TRefundDetail objTRefundDetail, AddCharge objAddCharge, Payment objPayment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };

            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(RefundId);
            objTRefundDetail.RefundId = Convert.ToInt32(RefundId);
            objAddCharge.ChargesId = Convert.ToInt32(RefundId);
            objPayment.RefundId = Convert.ToInt32(RefundId);

            string[] rRefundEntity = { "UpdatedBy", "RefundDetailsTime", "HospitalAmount", "DoctorAmount", "RefundDetId" };
            var RefundEntity = objTRefundDetail.ToDictionary();
            foreach (var rProperty in rRefundEntity)
            {
                RefundEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_T_RefundDetails_1", CommandType.StoredProcedure, RefundEntity);


            string[] rChargeEntity = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "PackageMainChargeId", "ClassId", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo" };
            var ChargeEntity = objAddCharge.ToDictionary();
            foreach (var rProperty in rChargeEntity)
            {
                ChargeEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);


            string[] rPayEntity = { "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount" };
            var PayEntity = objPayment.ToDictionary();
            foreach (var rProperty in rPayEntity)
            {
                PayEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure, PayEntity);


        }


        //    public virtual async Task InsertAsyncSP(
        //Refund objRefund,
        //TRefundDetail objTRefundDetail,
        //AddCharge objAddCharge,
        //Payment objPayment,
        //int userId,
        //string username)
        //    {
        //        DatabaseHelper odal = new();

        //        try
        //        {
        //            // Start a transaction
        //            await odal.BeginTransactionAsync();

        //            // Insert Refund
        //            string[] refundExcludedProperties = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };
        //            var refundEntity = objRefund.ToDictionary();
        //            foreach (var property in refundExcludedProperties)
        //            {
        //                refundEntity.Remove(property);
        //            }

        //            // Execute Refund insertion
        //            string refundId = await odal.ExecuteNonQueryAsync("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", refundEntity);
        //            objRefund.RefundId = Convert.ToInt32(refundId);

        //            // Set related entities with the new RefundId
        //            objTRefundDetail.RefundId = objRefund.RefundId;
        //            objAddCharge.ChargesId = objRefund.RefundId;
        //            objPayment.RefundId = objRefund.RefundId;

        //            // Insert TRefundDetail
        //            string[] refundDetailExcludedProperties = { "UpdatedBy", "RefundDetailsTime", "HospitalAmount", "DoctorAmount", "RefundDetId" };
        //            var refundDetailEntity = objTRefundDetail.ToDictionary();
        //            foreach (var property in refundDetailExcludedProperties)
        //            {
        //                refundDetailEntity.Remove(property);
        //            }
        //            await odal.ExecuteNonQueryAsync("m_insert_T_RefundDetails_1", CommandType.StoredProcedure, refundDetailEntity);

        //            // Insert AddCharge
        //            string[] chargeExcludedProperties = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "PackageMainChargeId", "ClassId", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo" };
        //            var chargeEntity = objAddCharge.ToDictionary();
        //            foreach (var property in chargeExcludedProperties)
        //            {
        //                chargeEntity.Remove(property);
        //            }
        //            await odal.ExecuteNonQueryAsync("m_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, chargeEntity);

        //            // Insert Payment
        //            string[] paymentExcludedProperties = { "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount" };
        //            var paymentEntity = objPayment.ToDictionary();
        //            foreach (var property in paymentExcludedProperties)
        //            {
        //                paymentEntity.Remove(property);
        //            }

        //            await odal.ExecuteNonQueryAsync("m_insert_Payment_1", CommandType.StoredProcedure, paymentEntity);

        //            // Commit transaction
        //            await odal.CommitTransactionAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            // Rollback transaction on error
        //            await odal.RollbackTransactionAsync();

        //            // Log the error (implement a logging framework if necessary)
        //            throw new Exception("An error occurred while inserting data into the database.", ex);
        //        }
        //    }
        public virtual async Task<long> InsertAsync(Refund objRefund, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };
            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("m_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(RefundId);

            //// Add details table records
            foreach (var objRefundDet in objRefund.TRefundDetails)
            {
                objRefundDet.RefundId = objRefund.RefundId;
            }
            _context.TRefundDetails.AddRange(objRefund.TRefundDetails);
            await _context.SaveChangesAsync();

            return objRefund.RefundId;
        }

    }
}
