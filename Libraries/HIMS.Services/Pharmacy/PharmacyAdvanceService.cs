using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class PharmacyAdvanceService : IPharmacyAdvanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PharmacyAdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task Insert(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // //Add header table records
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] rEntity = { "StoreId", "UnitId", "AdvanceId", "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AdvanceUsedAmount", "BalanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate" };
                var entity = ObjTPhadvanceHeader.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                string VAdvanceId = odal.ExecuteNonQuery("PS_insert_T_PHAdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
                ObjTPhadvanceHeader.AdvanceId = Convert.ToInt32(VAdvanceId);
                ObjTPhadvanceDetail.AdvanceId = Convert.ToInt32(VAdvanceId);
                await _context.LogProcedureExecution(entity, nameof(TPhadvanceHeader), Convert.ToInt32(ObjTPhadvanceHeader.AdvanceId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                string[] DEntity = { "Date", "Time", "UnitId", "AdvanceId", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "StoreId", "AdvanceDetailId" };
                var Dentity = ObjTPhadvanceDetail.ToDictionary();
                foreach (var rProperty in Dentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Dentity.Remove(rProperty);
                }
                string VAdvanceDetailID = odal.ExecuteNonQuery("ps_m_insert_TPHAdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", Dentity);
                ObjTPhadvanceDetail.AdvanceDetailId = Convert.ToInt32(VAdvanceDetailID);
                ObjPaymentPharmacy.AdvanceId = Convert.ToInt32(VAdvanceDetailID);
                await _context.LogProcedureExecution(entity, nameof(TPhadvanceDetail), Convert.ToInt32(ObjTPhadvanceDetail.AdvanceDetailId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                string[] PEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Opdipdtype", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
                var Entity = ObjPaymentPharmacy.ToDictionary();
                foreach (var rProperty in Entity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        Entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("PS_insert_I_PHPayment_1", CommandType.StoredProcedure, Entity);
                await _context.LogProcedureExecution(entity, nameof(PaymentPharmacy), Convert.ToInt32(ObjPaymentPharmacy.PaymentId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                foreach (var item in ObjTPaymentPharmacy)
                {
                    item.AdvanceId = Convert.ToInt32(VAdvanceDetailID);

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
                    await _context.LogProcedureExecution(entity, nameof(TPaymentPharmacy), Convert.ToInt32(item.PaymentId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                }
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback if anything fails
                await transaction.RollbackAsync();
                throw;
            }

        }
        public virtual async Task Update(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // //Add header table records
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] Entity = { "AdvanceAmount", "BalanceAmount", "AdvanceId" };
                var Uentity = ObjTPhadvanceHeader.ToDictionary();
                foreach (var rProperty in Uentity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        Uentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_m_Update_T_PHAdvanceHeader", CommandType.StoredProcedure, Uentity);
                await _context.LogProcedureExecution(Uentity, nameof(TPhadvanceHeader), Convert.ToInt32(ObjTPhadvanceHeader.AdvanceId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


                string[] DEntity = { "Date", "Time", "UnitId", "AdvanceId", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "StoreId", "AdvanceDetailId" };
                var Dentity = ObjTPhadvanceDetail.ToDictionary();
                foreach (var rProperty in Dentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Dentity.Remove(rProperty);
                }
                string VAdvanceDetailID = odal.ExecuteNonQuery("ps_m_insert_TPHAdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", Dentity);
                ObjTPhadvanceDetail.AdvanceDetailId = Convert.ToInt32(VAdvanceDetailID);
                ObjPaymentPharmacy.AdvanceId = Convert.ToInt32(VAdvanceDetailID);
                await _context.LogProcedureExecution(Uentity, nameof(TPhadvanceHeader), Convert.ToInt32(ObjTPhadvanceHeader.AdvanceId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                string[] PEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Opdipdtype", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
                var PPEntity = ObjPaymentPharmacy.ToDictionary();
                foreach (var rProperty in PPEntity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        PPEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("PS_insert_I_PHPayment_1", CommandType.StoredProcedure, PPEntity);
                await _context.LogProcedureExecution(PPEntity, nameof(TPhadvanceDetail), Convert.ToInt32(ObjTPhadvanceDetail.AdvanceDetailId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


                foreach (var item in ObjTPaymentPharmacy)
                {
                    item.AdvanceId = Convert.ToInt32(VAdvanceDetailID);


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
                    await _context.LogProcedureExecution(Uentity, nameof(PaymentPharmacy), Convert.ToInt32(ObjPaymentPharmacy.PaymentId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                }
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                // ❌ Rollback if anything fails
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}
