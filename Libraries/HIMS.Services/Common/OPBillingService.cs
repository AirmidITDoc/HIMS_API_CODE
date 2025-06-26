using Aspose.Cells.Drawing;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.Common
{
    public class OPBillingService : IOPBillingService
    {
        private readonly HIMSDbContext _context;
        public OPBillingService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<CertificateInformationListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CertificateInformationListDto>(model, "m_Rtrv_T_CertificateInformation_List");
        }
        public virtual async Task InsertAsync(TCertificateInformation ObjCertificateInformation, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TCertificateInformations.Add(ObjCertificateInformation);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task UpdateAsync(TCertificateInformation ObjCertificateInformation, int UserId, string Username)
        {
            // throw new NotImplementedException();
            _context.TCertificateInformations.Update(ObjCertificateInformation);
            _context.Entry(ObjCertificateInformation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //scope.Complete();
        }
        //public virtual async Task UpdateAsync(TCertificateInformation TCertificateInformation, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header & detail table records
        //        TCertificateInformation objReg = _context.TCertificateInformations.Where(x => x.CertificateId == TCertificateInformation.CertificateId).FirstOrDefault();
        //        if (objReg != null)
        //            _context.Entry(objReg).State = EntityState.Detached;

        //        TCertificateInformation.CertificateId = objReg.CertificateId;
        //        TCertificateInformation.CertificateDate = objReg.CertificateDate;
        //        TCertificateInformation.CertificateTime = objReg.CertificateTime;
        //        TCertificateInformation.VisitId = objReg.VisitId;
        //        //TCertificateInformation.CertificateTempId = objReg.CertificateTempId;
        //        TCertificateInformation.CertificateName = objReg.CertificateName;
        //        TCertificateInformation.CertificateText = objReg.CertificateText;
        //        //TCertificateInformation.ModifiedBy = objReg.ModifiedBy;
              
        //        _context.TCertificateInformations.Update(TCertificateInformation);
        //        _context.Entry(TCertificateInformation).State = EntityState.Modified;

        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

        public virtual async Task InsertAsyncSP(Bill objBill, Payment objPayment, int CurrentUserId, string CurrentUserName)
        {

            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
                objBill.BillNo = Convert.ToInt32(vBillNo);

                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    foreach (var objItem1 in objBill.AddCharges)
                    {
                        // Add Charges Code
                        objItem1.BillNo = objBill.BillNo;
                        objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                        objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                        objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        // Bill Details Code
                            BillDetail objBillDet = new BillDetail();
                            objBillDet.BillNo = objBill.BillNo;
                            objBillDet.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objBillDet);
                            await _context.SaveChangesAsync();

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new TPathologyReportHeader();
                            objPatho.PathDate = objItem1.ChargesDate;
                            objPatho.PathTime = objItem1?.ChargesDate;
                            objPatho.OpdIpdType = objItem1?.OpdIpdType;
                            objPatho.OpdIpdId = objItem1?.OpdIpdId;
                            objPatho.PathTestId = objItem1?.ServiceId;
                            objPatho.AddedBy = objItem1?.AddedBy;
                            objPatho.ChargeId = objItem1?.ChargesId;
                            objPatho.IsCompleted = false;
                            objPatho.IsPrinted = false;
                            objPatho.IsSampleCollection = false;
                            objPatho.TestType = false;

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new TRadiologyReportHeader();
                            objRadio.RadDate = objItem1.ChargesDate;
                            objRadio.RadTime = objItem1?.ChargesDate;
                            objRadio.OpdIpdType = objItem1?.OpdIpdType;
                            objRadio.OpdIpdId = objItem1?.OpdIpdId;
                            objRadio.RadTestId = objItem1?.ServiceId;
                            objRadio.AddedBy = objItem1?.AddedBy;
                            objRadio.ChargeId = objItem1?.ChargesId;
                            objRadio.IsCompleted = false;
                            objRadio.IsCancelled = 0;
                            objRadio.IsPrinted = false;
                            objRadio.TestType = false;

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }

                    }

                    string[] rPaymentEntity = { "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "Tdsamount", "BillNoNavigation" };
                    Payment objPay = new Payment();
                    objPay = objPayment;
                    objPay.BillNo = objBill.BillNo;
                    var entity2 = objPayment.ToDictionary();
                    foreach (var rProperty in rPaymentEntity)
                    {
                        entity2.Remove(rProperty);
                    }
                    string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
                    objPayment.PaymentId = Convert.ToInt32(PaymentId);
                
                    scope.Complete();
                }

            }

            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }


        public virtual async Task InsertAsyncSP1(Bill objBill, int CurrentUserId, string CurrentUserName)
        {
            try
            {
                // Bill Code
                DatabaseHelper odal = new();
                string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
                objBill.BillNo = Convert.ToInt32(vBillNo);

                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    foreach (var objItem1 in objBill.AddCharges)
                    {
                        // Add Charges Code
                        objItem1.BillNo = objBill.BillNo;
                        objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                        objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                        objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();
                        
                        // Bill Details Code
                        BillDetail objBillDet = new BillDetail();
                        objBillDet.BillNo = objBill.BillNo;
                        objBillDet.ChargesId = objItem1?.ChargesId;
                        _context.BillDetails.Add(objBillDet);
                        await _context.SaveChangesAsync();

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new TPathologyReportHeader();
                            objPatho.PathDate = objItem1.ChargesDate;
                            objPatho.PathTime = objItem1?.ChargesDate;
                            objPatho.OpdIpdType = objItem1?.OpdIpdType;
                            objPatho.OpdIpdId = objItem1?.OpdIpdId;
                            objPatho.PathTestId = objItem1?.ServiceId;
                            objPatho.AddedBy = objItem1?.AddedBy;
                            objPatho.ChargeId = objItem1?.ChargesId;
                            objPatho.IsCompleted = false;
                            objPatho.IsPrinted = false;
                            objPatho.IsSampleCollection = false;
                            objPatho.TestType = false;

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new TRadiologyReportHeader();
                            objRadio.RadDate = objItem1.ChargesDate;
                            objRadio.RadTime = objItem1?.ChargesDate;
                            objRadio.OpdIpdType = objItem1?.OpdIpdType;
                            objRadio.OpdIpdId = objItem1?.OpdIpdId;
                            objRadio.RadTestId = objItem1?.ServiceId;
                            objRadio.AddedBy = objItem1?.AddedBy;
                            objRadio.ChargeId = objItem1?.ChargesId;
                            objRadio.IsCompleted = false;
                            objRadio.IsCancelled = 0;
                            objRadio.IsPrinted = false;
                            objRadio.TestType = false;

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                    }

                   scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task InsertCreditBillAsyncSP(Bill objBill, int currentUserId, string currentUserName)
        {
            // throw new NotImplementedException();
            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
                objBill.BillNo = Convert.ToInt32(vBillNo);



                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    foreach (var objItem1 in objBill.AddCharges)
                    {
                        objItem1.BillNo = objBill.BillNo;
                        objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                        objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                        objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);

                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        foreach (var objItem in objBill.BillDetails)
                        {
                            objItem.BillNo = objBill.BillNo;
                            objItem.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objItem);
                            await _context.SaveChangesAsync();

                        }

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new TPathologyReportHeader();
                            objPatho.PathDate = objItem1.ChargesDate;
                            objPatho.PathTime = objItem1?.ChargesDate;
                            objPatho.OpdIpdType = objItem1?.OpdIpdType;
                            objPatho.OpdIpdId = objItem1?.OpdIpdId;
                            objPatho.PathTestId = objItem1?.ServiceId;
                            objPatho.AddedBy = objItem1?.AddedBy;
                            objPatho.ChargeId = objItem1?.ChargesId;
                            objPatho.IsCompleted = false;
                            objPatho.IsPrinted = false;
                            objPatho.IsSampleCollection = false;
                            objPatho.TestType = false;

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new TRadiologyReportHeader();
                            objRadio.RadDate = objItem1.ChargesDate;
                            objRadio.RadTime = objItem1?.ChargesDate;
                            objRadio.OpdIpdType = objItem1?.OpdIpdType;
                            objRadio.OpdIpdId = objItem1?.OpdIpdId;
                            objRadio.RadTestId = objItem1?.ServiceId;
                            objRadio.AddedBy = objItem1?.AddedBy;
                            objRadio.ChargeId = objItem1?.ChargesId;
                            objRadio.IsCompleted = false;
                            objRadio.IsCancelled = 0;
                            objRadio.IsPrinted = false;
                            objRadio.TestType = false;

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                    }
                   
                }
            }


            catch (Exception)
            {
                Bill objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }
    }
}
