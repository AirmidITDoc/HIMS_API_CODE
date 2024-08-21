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

namespace HIMS.Services.OutPatient
{
    public class OPBillingService : IOPBillingService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPBillingService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName)
        {
            //      m_insert_Bill_1
            //      m_insert_OPAddCharges_1
            //      m_insert_BillDetails_1
            //      m_insert_PathologyReportHeader_1
            //      m_insert_RadiologyReportHeader_1
            //      m_insert_Payment_1

            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo" };
            var entity = objBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("m_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
            objBill.BillNo = Convert.ToInt32(vBillNo);

            foreach (var objItem1 in objBill.AddCharges)
            {
                objItem1.BillNo = objBill.BillNo;
                _context.AddCharges.AddRange(objBill.AddCharges);

                foreach (var objItem in objBill.BillDetails)
                {
                    objItem.BillNo = objBill.BillNo;
                    objItem.ChargesId = objItem1?.ChargesId;
                }
                _context.BillDetails.AddRange(objBill.BillDetails);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
            }


            //string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            //var visitentity = objVisitDetail.ToDictionary();
            //foreach (var rProperty in rVisitEntity)
            //{
            //    visitentity.Remove(rProperty);
            //}
            //string VisitId = odal.ExecuteNonQuery("m_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            //objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            //SqlParameter[] para = new SqlParameter[1];
            //para[0] = new SqlParameter
            //{
            //    SqlDbType = SqlDbType.BigInt,
            //    ParameterName = "@PatVisitID",
            //    Value = Convert.ToInt32(VisitId)
            //};
            //odal.ExecuteNonQuery("m_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, para);
        }
    }
}
