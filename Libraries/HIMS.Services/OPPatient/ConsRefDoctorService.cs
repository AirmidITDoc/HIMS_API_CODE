using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.OPPatient
{
    public class ConsRefDoctorService : IConsRefDoctorService
    {
        private readonly HIMSDbContext _context;
        public ConsRefDoctorService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task UpdateAsync(VisitDetail objVisitDetail, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegId", "VisitDate", "VisitTime", "UnitId", "PatientTypeId", "RefDocId", "Opdno", "TariffId", "CompanyId", "AddedBy", "UpdatedBy",
                "IsCancelledBy","IsCancelled","IsCancelledDate","ClassId","PatientOldNew","FirstFollowupVisit","AppPurposeId","FollowupDate","IsMark","Comments","IsXray","CrossConsulFlag",
                "PhoneAppId","Height","Pweight","Bmi","Bsl","SpO2","Temp","Pulse","Bp","CheckInTime","CheckOutTime","ConStartTime",
                "ConEndTime","CampId","CrossConsultantDrId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","IsConvertRequestForIp"};
            var entity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_ConsultationDoctor_Visit", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);

        }

        public virtual async Task Update(VisitDetail objVisitDetail, int UserId, string Username)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "RefDocId", "VisitId"};
                var entity = objVisitDetail.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_RefDoctor_Visit", CommandType.StoredProcedure, entity);
                // Audit Log 
                await _context.LogProcedureExecution(entity, nameof(VisitDetail), objVisitDetail.VisitId.ToInt(), Core.Domain.Logging.LogAction.Edit, UserId, Username);
                // Save
                await _context.SaveChangesAsync(UserId, Username);
                // Commit
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
        
    }
}


