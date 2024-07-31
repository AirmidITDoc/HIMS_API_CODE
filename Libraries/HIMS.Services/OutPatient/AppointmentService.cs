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

        public virtual async Task InsertAsyncSP(Registration objRegistration,VisitDetail objVisitDetail, int UserId, string Username)
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

            await _context.SaveChangesAsync(UserId, Username);

            string[] rVisitEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var visitentity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                entity.Remove(rProperty);
            }
            string VisitID = odal.ExecuteNonQuery("m_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitID", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitID);


            await _context.SaveChangesAsync(UserId, Username);
            string[] rVisitentity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            var visitEntity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rVisitEntity)
            {
                entity.Remove(rProperty);
            }
            string VisitId = odal.ExecuteNonQuery("m_update_RegistrationForAppointment_1", CommandType.StoredProcedure, "VisitID", visitentity);
            objVisitDetail.VisitId = Convert.ToInt32(VisitID);


            await _context.SaveChangesAsync(UserId, Username);
        }

        public Task InsertAsyncSP(VisitDetail model, int currentUserId, string currentUserName)
        {
            throw new NotImplementedException();
        }

       
    }
}
