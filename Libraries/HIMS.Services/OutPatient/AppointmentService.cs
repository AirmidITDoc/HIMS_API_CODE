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
    public class AppointmentService : IAppointmentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AppointmentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
      
        public virtual async Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("m_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);

            string[] rVisitEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitID = odal.ExecuteNonQuery("m_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitID", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitID);

            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

        }

        public virtual async Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("m_update_RegistrationForAppointment_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);

            string[] rVisitEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                visitentity.Remove(rProperty);
            }
            string VisitID = odal.ExecuteNonQuery("m_update_RegistrationForAppointment_1", CommandType.StoredProcedure, "VisitID", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitID);


            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
        }
        public virtual async Task CancelAsyncSP(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                VisitDetail objVisit = await _context.VisitDetails.FindAsync(objVisitDetail.VisitId);
                objVisit.IsCancelled= objVisitDetail.IsCancelled;
                objVisit.IsCancelledBy = objVisitDetail.IsCancelledBy;
                objVisit.IsCancelledDate = objVisitDetail.IsCancelledDate;
                _context.VisitDetails.Update(objVisit);
                _context.Entry(objVisit).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

     

      

      
    }
}
