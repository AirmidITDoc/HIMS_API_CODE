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
using System.Transactions;

namespace HIMS.Services.Common
{
    public class IPBIllService : IIPBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPBIllService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName)
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
                string vBillNo = odal.ExecuteNonQuery("v_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
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
                        foreach (var objItem in objBill.BillDetails)
                        {
                            objItem.BillNo = objBill.BillNo;
                            objItem.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objItem);
                            await _context.SaveChangesAsync();
                        }

                        //m_update_AdvanceDetail_1

                        //DatabaseHelper odal = new();
                        //string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                        //var entity = objBill.ToDictionary();
                        //foreach (var rProperty in rEntity)
                        //{
                        //    entity.Remove(rProperty);
                        //}
                        //odal.ExecuteNonQuery("m_update_AdvanceDetail_1", CommandType.StoredProcedure, entity);

                        //AdvanceDetail objAdvanceDetail = new AdvanceDetail();
                        //objAdvanceDetail.AdvanceDetailId = 74418;
                        //objAdvanceDetail.BalanceAmount = objBill.BalanceAmt;
                        //objAdvanceDetail.UsedAmount = 11;// objBill.AdvanceUsedAmount;
                        //_context.AdvanceDetails.Add(objAdvanceDetail);
                        //await _context.SaveChangesAsync();

                    }

                    // Payment Code
                    int _val = 0;
                    foreach (var objPayment in objBill.Payments)
                    {
                        if (_val == 0)
                        {
                            objPayment.BillNo = objBill.BillNo;
                            _context.Payments.Add(objPayment);
                            await _context.SaveChangesAsync();
                        }
                        _val += 1;
                    }
                    scope.Complete();

                }

                //m_Cal_DiscAmount_OPBill
                //DatabaseHelper odal1 = new();
                //AddCharge objAddcharges = new AddCharge();
                //objAddcharges.BillNo = Convert.ToInt32(vBillNo);
                //string[] rEntity1 = { "ChargesId", "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId",
                //"IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount", "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","BillNoNavigation"};
                //var entity1 = objAddcharges.ToDictionary();
                //foreach (var rProperty in rEntity1)
                //{
                //    entity1.Remove(rProperty);
                //}
                //odal.ExecuteNonQuery("m_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, entity1);

                // //m_update_T_AdmissionforIPBilling
                //DatabaseHelper odal2 = new();
                //Admission objAdmission = new Admission();
                //string[] rEntity2 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                //var entity2 = objAdmission.ToDictionary();
                //foreach (var rProperty in rEntity2)
                //{
                //    entity2.Remove(rProperty);
                //}
                //odal.ExecuteNonQuery("m_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);
                //Edmx
                //Admission objAdmission = new Admission();
                //objAdmission.AdmissionId = objBill.OpdIpdId;
                //_context.Admissions.Add(objAdmission);
                //await _context.SaveChangesAsync();


                //m_update_BillBalAmount_1
                DatabaseHelper odal3 = new();
                string[] rEntity3 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity3 = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity3.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_BillBalAmount_1", CommandType.StoredProcedure, entity3);
                //Edmx
                //Bill objBill1 = new Bill();
                //objBill1.BillNo = Convert.ToInt32(vBillNo);
                ////_context.Bill.Add(objBill1);
                //await _context.SaveChangesAsync();


                // //objBill.BillNo = Convert.ToInt32(vBillNo);
                //Admission objAdmission1 = new Admission();
                //_context.Admissions.Add(objAdmission1);
                //await _context.SaveChangesAsync();

                //AdvanceHeader objAdvHeader = new AdvanceHeader();
                //_context.AdvanceHeaders.Add(objAdvHeader);
                //await _context.SaveChangesAsync();

                //AdvanceDetail objAdvDetail = new AdvanceDetail();
                //_context.AdvanceDetails.Add(objAdvDetail);
                //await _context.SaveChangesAsync();

            }

            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task InsertCreditBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();

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
                string vBillNo = odal.ExecuteNonQuery("v_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
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
                        foreach (var objItem in objBill.BillDetails)
                        {
                            objItem.BillNo = objBill.BillNo;
                            objItem.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objItem);
                            await _context.SaveChangesAsync();
                        }

                        //m_update_AdvanceDetail_1
                        //foreach (var a in objBill.IPAdvanceDetailUpdate)
                        //{
                        //    DatabaseHelper odal = new();
                        //    string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                        //    var entity = objBill.ToDictionary();
                        //    foreach (var rProperty in rEntity)
                        //    {
                        //        entity.Remove(rProperty);
                        //    }
                        //    odal.ExecuteNonQuery("m_update_AdvanceDetail_1", CommandType.StoredProcedure, entity);

                        //}

                    }


                    scope.Complete();

                }

                //m_Cal_DiscAmount_OPBill
                //DatabaseHelper odal1 = new();
                //string[] rEntity1 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                //var entity1 = objBill.ToDictionary();
                //foreach (var rProperty in rEntity1)
                //{
                //    entity1.Remove(rProperty);
                //}
                //odal.ExecuteNonQuery("m_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, entity1);

                // //m_update_T_AdmissionforIPBilling
                // DatabaseHelper odal2 = new();
                // string[] rEntity2 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                // var entity2 = objBill.ToDictionary();
                // foreach (var rProperty in rEntity2)
                // {
                //     entity2.Remove(rProperty);
                // }
                // odal.ExecuteNonQuery("m_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);
                // //objBill.BillNo = Convert.ToInt32(vBillNo);


                // //m_update_BillBalAmount_1
                // DatabaseHelper odal3 = new();
                // string[] rEntity3 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                // var entity3 = objBill.ToDictionary();
                // foreach (var rProperty in rEntity)
                // {
                //     entity3.Remove(rProperty);
                // }
                // odal.ExecuteNonQuery("m_update_BillBalAmount_1", CommandType.StoredProcedure, entity3);
                // //objBill.BillNo = Convert.ToInt32(vBillNo);

                // //m_update_AdvanceHeader_1
                // DatabaseHelper odal4 = new();
                // string[] rEntity4 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                // var entity4 = objBill.ToDictionary();
                // foreach (var rProperty in rEntity)
                // {
                //     entity4.Remove(rProperty);
                // }
                //odal.ExecuteNonQuery("m_update_AdvanceHeader_1", CommandType.StoredProcedure, entity4);
                //// objBill.BillNo = Convert.ToInt32(vBillNo);


            }

            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }
    }

}