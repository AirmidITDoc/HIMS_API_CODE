using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

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



        //public virtual void InsertIP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, int UserId, string Username)
        //{

        //    DatabaseHelper odal = new();
        //    string[] rEntity = { "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails", "AddBy" };

        //    var entity = objRefund.ToDictionary();
        //    foreach (var rProperty in rEntity)
        //    {
        //        entity.Remove(rProperty);
        //    }
        //    string vRefundId = odal.ExecuteNonQuery("ps_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
        //    objRefund.RefundId = Convert.ToInt32(vRefundId);
        //    objPayment.RefundId = Convert.ToInt32(vRefundId);

        //    foreach (var item in objTRefundDetail)
        //    {
        //        item.RefundId = Convert.ToInt32(vRefundId);
        //        string[] rRefundEntity = { "HospitalAmount", "DoctorAmount", "RefundDetId", "UpdatedBy", "Refund" };
        //        var RefundEntity = item.ToDictionary();
        //        foreach (var rProperty in rRefundEntity)
        //        {
        //            RefundEntity.Remove(rProperty);
        //        }
        //        odal.ExecuteNonQuery("ps_insert_T_RefundDetails_1", CommandType.StoredProcedure, RefundEntity);
        //    }

        //    foreach (var item in objAddCharge)
        //    {
        //        string[] rChargeEntity = { "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt",
        //            "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsDoctorShareGenerated", "IsInterimBillFlag", "IsPackage", "IsSelfOrCompanyService", "PackageId", "ChargesTime",
        //            "PackageMainChargeId", "ClassId", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "IsHospMrk", "BillNoNavigation",
        //        "UnitId","TariffId","DoctorName","WardId","BedId","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
        //        var ChargeEntity = item.ToDictionary();
        //        foreach (var rProperty in rChargeEntity)
        //        {
        //            ChargeEntity.Remove(rProperty);
        //        }
        //        odal.ExecuteNonQuery("ps_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);

        //    }

        //    string[] rPayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
        //    var PayEntity = objPayment.ToDictionary();
        //    foreach (var rProperty in rPayEntity)
        //    {
        //        PayEntity.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, PayEntity);
        //}
        public virtual async Task  InsertIP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "RefundDate", "RefundTime", "RefundNo", "BillId", "AdvanceId", "OpdIpdType", "OpdIpdId", "RefundAmount", "Remark", "TransactionId", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "RefundId", "UnitId" };

            var entity = objRefund.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string vRefundId = odal.ExecuteNonQuery("ps_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(vRefundId);
            objPayment.RefundId = Convert.ToInt32(vRefundId);
            foreach (var item in objTRefundDetail)
            {
                item.RefundId = Convert.ToInt32(vRefundId);
                string[] rRefundEntity = { "RefundId", "ServiceId", "ServiceAmount", "RefundAmount", "DoctorId", "Remark", "AddBy", "ChargesId" };
                var RefundEntity = item.ToDictionary();
                foreach (var rProperty in RefundEntity.Keys.ToList())
                {
                    if (!rRefundEntity.Contains(rProperty))
                        RefundEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_RefundDetails_1", CommandType.StoredProcedure, RefundEntity);
                //await _context.LogProcedureExecution(RefundEntity, nameof(TRefundDetail), objRefund.RefundId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            }

            foreach (var item in objAddCharge)
            {
                string[] rChargeEntity = { "ChargesId", "RefundAmount" };
                var ChargeEntity = item.ToDictionary();
                foreach (var rProperty in ChargeEntity.Keys.ToList())
                {
                    if (!rChargeEntity.Contains(rProperty))
                        ChargeEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);
                //await _context.LogProcedureExecution(ChargeEntity, nameof(AddCharge), item.ChargesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            }


            string[] rPayEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId","RefundId","TransactionType","Remark","AddBy","IsCancelled","IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var PayEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity.Keys.ToList())
            {
                if (!rPayEntity.Contains(rProperty))
                    PayEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, PayEntity);

            foreach (var item in ObjTPayment)
            {
                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy", "TransactionLabel"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);

            }
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

        }

        public virtual async Task InsertOP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "RefundDate", "RefundTime", "RefundNo", "BillId", "AdvanceId", "OpdIpdType", "OpdIpdId", "RefundAmount", "Remark", "TransactionId", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "RefundId", "UnitId", "CashCounterId" };

            var entity = objRefund.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string vRefundId = odal.ExecuteNonQuery("ps_insert_Refund_1", CommandType.StoredProcedure, "RefundId", entity);
            objRefund.RefundId = Convert.ToInt32(vRefundId);
            objPayment.RefundId = Convert.ToInt32(vRefundId);

            foreach (var item in objTRefundDetail)
            {
                item.RefundId = Convert.ToInt32(vRefundId);
                string[] rRefundEntity = { "RefundId", "ServiceId", "ServiceAmount", "RefundAmount", "DoctorId", "Remark", "AddBy", "ChargesId" };
                var RefundEntity = item.ToDictionary();
                foreach (var rProperty in RefundEntity.Keys.ToList())
                {
                    if (!rRefundEntity.Contains(rProperty))
                        RefundEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_RefundDetails_1", CommandType.StoredProcedure, RefundEntity);
                //await _context.LogProcedureExecution(RefundEntity, nameof(TRefundDetail), objRefund.RefundId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            }

            foreach (var item in objAddCharge)
            {
                string[] rChargeEntity = { "ChargesId", "RefundAmount"};
                var ChargeEntity = item.ToDictionary();
                foreach (var rProperty in ChargeEntity.Keys.ToList())
                {
                    if (!rChargeEntity.Contains(rProperty))
                        ChargeEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_AddCharges_RefundAmt", CommandType.StoredProcedure, ChargeEntity);

            }

            string[] rPayEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var PayEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity.Keys.ToList())
            {
                if (!rPayEntity.Contains(rProperty))
                    PayEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_Refund_1", CommandType.StoredProcedure, PayEntity);
            //await _context.LogProcedureExecution(PayEntity, nameof(Payment), objPayment.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            foreach (var items in ObjTPayment)
            {
                items.RefundId = Convert.ToInt32(vRefundId);

                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};
                var pentity = items.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                items.PaymentId = Convert.ToInt32(VPaymentId);

            }

        }


        public virtual void InsertIP(Refund objRefund, TRefundDetail objTRefundDetail, AddCharge objAddCharge, Payment objPayment, int UserId, string Username)
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
