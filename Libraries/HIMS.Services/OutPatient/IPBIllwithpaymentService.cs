﻿using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class IPBIllwithpaymentService : IIPBIllwithpaymentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPBIllwithpaymentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Bill objBill,int CurrentUserId, string CurrentUserName)
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
                string vBillNo = odal.ExecuteNonQuery("m_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
                objBill.BillNo = Convert.ToInt32(vBillNo);

                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    //foreach (var objItem1 in objBill.Bil)
                    //{

                        //Bill detail
                        DatabaseHelper odal1 = new();
                        string[] rEntity1 = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "BillDate", "OpdIpdType", "TotalAdvanceAmount", "AddedBy", "BillTime", "BillYear", "PrintBillNo", "AddCharges", "Bill", "Payments" };
                        var entity1 = objBill.ToDictionary();
                        foreach (var rProperty in rEntity)
                        {
                            entity1.Remove(rProperty);
                        }
                        odal.ExecuteNonQuery("m_insert_BillDetails_1", CommandType.StoredProcedure, entity1);
                        
                       


                        // m_update_AdvanceDetail_1
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

                    //}

                    // Payment Code
                    int _val = 0;
                    //foreach (var objPayment in objBill.Payments)
                    //{
                    //    if (_val == 0)
                    //    {
                    //        objPayment.BillNo = objBill.BillNo;
                    //        _context.Payments.Add(objPayment);
                    //        await _context.SaveChangesAsync();
                    //    }
                    //    _val += 1;
                    //}

                    //scope.Complete();

                }

                //m_Cal_DiscAmount_OPBill
                //DatabaseHelper odal1 = new();
                //string[] rEntity1 = { "ChargesId", "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty ", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId" };
                //var entity1 = objAddcharges.ToDictionary();
                //foreach (var rProperty in rEntity1)
                //{
                //    entity1.Remove(rProperty);
                //}
                //odal.ExecuteNonQuery("m_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, entity1);

                // //m_update_T_AdmissionforIPBilling
                //DatabaseHelper odal2 = new();
                //string[] rEntity2 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                //var entity2 = objBill.ToDictionary();
                //foreach (var rProperty in rEntity2)
                //{
                //    entity2.Remove(rProperty);
                //}
                //odal.ExecuteNonQuery("m_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);
                //objBill.BillNo = Convert.ToInt32(vBillNo);


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


