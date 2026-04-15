using HIMS.Core.Domain.Logging;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace HIMS.Services.Pharmacy
{
    public class PharmacyRefundService : IPharmacyRefundService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PharmacyRefundService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task Insert(TPhRefund ObjTPhRefund, TPhadvanceHeader ObjTPhadvanceHeader, List<TPhadvRefundDetail> ObjTPhadvRefundDetail, List<TPhadvanceDetail> ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "RefundDate", "RefundTime", "BillId", "AdvanceId", "OpdIpdType", "OpdIpdId", "RefundAmount", "Remark", "TransactionId", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "StrId", "RefundId" };
            var entity = ObjTPhRefund.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VRefundId = odal.ExecuteNonQuery("PS_insert_T_PhAdvRefund_1", CommandType.StoredProcedure, "RefundId", entity);
            ObjTPhRefund.RefundId = Convert.ToInt32(VRefundId);
            ObjPaymentPharmacy.RefundId = Convert.ToInt32(VRefundId);
            //await _context.LogProcedureExecution(entity, nameof(TPhRefund), Convert.ToInt32(ObjTPhRefund.RefundId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(entity, nameof(TPhRefund), Convert.ToInt32(ObjTPhRefund.RefundId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            string[] AEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };
            var Aentity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in Aentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Aentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_PhAdvanceHeader_1", CommandType.StoredProcedure, Aentity);
            //await _context.LogProcedureExecution(entity, nameof(TPhadvanceHeader), Convert.ToInt32(ObjTPhadvanceHeader.AdvanceId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(Aentity, nameof(TPhadvanceHeader), Convert.ToInt32(ObjTPhadvanceHeader.AdvanceId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            foreach (var item in ObjTPhadvRefundDetail)
            {

                string[] DEntity = { "AdvDetailId", "RefundDate", "RefundTime", "AdvRefundAmt" };
                var Dentity = item.ToDictionary();
                foreach (var rProperty in Dentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_insert_T_PHAdvRefundDetail_1", CommandType.StoredProcedure, Dentity);
                //await _context.LogProcedureExecution(entity, nameof(TPhadvRefundDetail), Convert.ToInt32(item.AdvRefId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                _ = Task.Run(() => _context.LogProcedureExecution(Dentity, nameof(TPhadvRefundDetail), Convert.ToInt32(item.AdvRefId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            }

            foreach (var item in ObjTPhadvanceDetail)
            {

                string[] PEntity = { "AdvanceDetailId", "BalanceAmount", "RefundAmount" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in Pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PHAdvanceDetailBalAmount_1", CommandType.StoredProcedure, Pentity);
                //await _context.LogProcedureExecution(entity, nameof(TPhadvanceDetail), Convert.ToInt32(item.AdvanceDetailId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                _ = Task.Run(() => _context.LogProcedureExecution(Pentity, nameof(TPhadvanceDetail), Convert.ToInt32(item.AdvanceDetailId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            }
            string[] PHEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType",
                "Opdipdtype", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
            var Entity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in Entity.Keys.ToList())
            {
                if (!PHEntity.Contains(rProperty))
                    Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("insert_I_PHPayment_1", CommandType.StoredProcedure, Entity);
            //await _context.LogProcedureExecution(entity, nameof(PaymentPharmacy), Convert.ToInt32(ObjPaymentPharmacy.PaymentId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            _ = Task.Run(() => _context.LogProcedureExecution(Entity, nameof(PaymentPharmacy), Convert.ToInt32(ObjPaymentPharmacy.PaymentId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));



            foreach (var item in ObjTPaymentPharmacy)
            {
                item.RefundId = Convert.ToInt32(VRefundId);
                string[] SEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!SEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_PaymentPharmacy", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);
                //await _context.LogProcedureExecution(entity, nameof(TPaymentPharmacy), Convert.ToInt32(item.PaymentId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                _ = Task.Run(() => _context.LogProcedureExecution(pentity, nameof(TPaymentPharmacy), Convert.ToInt32(item.PaymentId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName));

            }
        }
    }
}
