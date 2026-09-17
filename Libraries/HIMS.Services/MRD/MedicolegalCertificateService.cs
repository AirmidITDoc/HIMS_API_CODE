using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
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

namespace HIMS.Services.MRD
{
    public partial class MedicolegalCertificateService : IMedicolegalCertificateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MedicolegalCertificateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TMedicolegalCertificate ObjTMedicolegalCertificate,int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] Entity = { "DocId", "Mlcdate", "Mlctime", "CertificateNo", "OpIpId", "OpIpType", "AccidentDate", "AccidentTime", "DetailsInjuries", "AgeofInjuries",
               "CauseofInjuries","TreatingDoctorId","TreatingDoctorId1","TreatingDoctorId2","DepartmentId","ClassId","DepartmentId","Mlcid","AdmissionId","IsEmgOrAdm","Mlcno","ReportingDate","ReportingTime","AuthorityName",
               "BuckleNo","PoliceStation","DetailGiven","Remark"};
                var entity = ObjTMedicolegalCertificate.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                string VDocId = odal.ExecuteNonQuery("ps_MedicolegalCertificate_Insert", CommandType.StoredProcedure, "DocId", entity);
                ObjTMedicolegalCertificate.DocId = Convert.ToInt32(VDocId);
                await _context.LogProcedureExecution(entity, nameof(TMedicolegalCertificate), ObjTMedicolegalCertificate.DocId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                // Save audit log changes
                await _context.SaveChangesAsync();
                // Commit transaction
                await transaction.CommitAsync();
            }
            catch
            {
                // Rollback transaction on error
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
