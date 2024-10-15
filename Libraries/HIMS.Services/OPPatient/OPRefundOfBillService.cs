using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public class OPRefundOfBillService : IOPRefundOfBillService
    {
        private readonly HIMSDbContext _context;
        public OPRefundOfBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncOP(Refund objRefund, TRefundDetail objTRefundDetail, AddCharge objAddCharge, Payment objPayment, int UserId, string Username)
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
