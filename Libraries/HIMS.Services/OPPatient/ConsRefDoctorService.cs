using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            "IsCancelledBy","IsCancelled","IsCancelledDate","ClassId","PatientOldNew","FirstFollowupVisit","AppPurposeId","FollowupDate","IsMark","Comments","IsXray","CrossConsulFlag","PhoneAppId" };
            var entity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
             odal.ExecuteNonQuery("Update_ConsultationDoctor", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);

        }

        public virtual async Task Update(VisitDetail objVisitDetail, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegId", "VisitDate", "VisitTime", "UnitId", "PatientTypeId", "ConsultantDocId","DepartmentId", "Opdno", "TariffId", "CompanyId", "AddedBy", "UpdatedBy",
            "IsCancelledBy","IsCancelled","IsCancelledDate","ClassId","PatientOldNew","FirstFollowupVisit","AppPurposeId","FollowupDate","IsMark","Comments","IsXray","CrossConsulFlag","PhoneAppId" };
            var entity = objVisitDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
             odal.ExecuteNonQuery("Update_RefranceDoctor", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);

        }
    }
}
