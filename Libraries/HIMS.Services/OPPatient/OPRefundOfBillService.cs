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
using HIMS.Data.DTO.IPPatient;

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
            return await DatabaseHelper.GetGridDataBySp<OpBilllistforRefundDto>(model, "ps_Rtrv_OPBillListforRefund");
        }
        public virtual async Task<IPagedList<OPBillservicedetailListDto>> GetBillservicedetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OPBillservicedetailListDto>(model, "ps_rtrv_OPBill_For_Refund");
        }

        public virtual async Task<IPagedList<RefundAgainstBillListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RefundAgainstBillListDto>(model, "ps_rtrv_OPDRefundAgainstBillList");
        }

        public virtual async Task<IPagedList<IPBillListforRefundListDto>> IPBillGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillListforRefundListDto>(model, "ps_IPBillListforRefund");
        }

        public virtual async Task<IPagedList<IPBillForRefundListDto>> IPBillForRefundListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillForRefundListDto>(model, "ps_Rtrv_IPBill_For_Refund");
        }



        public virtual async Task InsertAsyncIP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails", "AddBy" };

            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vRefundId = odal.ExecuteNonQuery("ps_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
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
                string[] rChargeEntity = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "PackageMainChargeId", "ClassId", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "IsHospMrk", "BillNoNavigation" };
                var ChargeEntity = item.ToDictionary();
                foreach (var rProperty in rChargeEntity)
                {
                    ChargeEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);

            }

            string[] rPayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var PayEntity = objPayment.ToDictionary();
            foreach (var rProperty in rPayEntity)
            {
                PayEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, PayEntity);
        }

        public virtual async Task InsertAsyncOP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails", "AddBy" };

            var entity = objRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vRefundId = odal.ExecuteNonQuery("ps_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
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
                string[] rChargeEntity = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", 
                    "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "PackageMainChargeId", 
                    "ClassId", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "IsHospMrk", "BillNoNavigation",
                "UnitId","TariffId","DoctorName","WardId","BedId","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
                var ChargeEntity = item.ToDictionary();
                foreach (var rProperty in rChargeEntity)
                {
                    ChargeEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);

            }

            string[] rPayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var PayEntity = objPayment.ToDictionary();
            foreach (var rProperty in rPayEntity)
            {
                PayEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_Refund_1", CommandType.StoredProcedure, PayEntity);
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
