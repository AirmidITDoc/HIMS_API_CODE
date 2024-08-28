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
            //// NEW CODE With EDMX
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Add Registration table records
                _context.Registrations.Add(objRegistration);
                await _context.SaveChangesAsync();

                // Update Registration table records
                ConfigSetting objConfigRSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigRSetting.RegNo = Convert.ToString(Convert.ToInt32(objConfigRSetting.RegNo) + 1);
                _context.ConfigSettings.Update(objConfigRSetting);
                _context.Entry(objConfigRSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Add VisitDetail table records
                objVisitDetail.RegId = objRegistration.RegId;
                _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();

                // Update VisitDetail table records
                ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Add TokenNumber table records
                List<VisitDetail> objVisit = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
                foreach (var item in objVisit)
                {
                    TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                    if (objToken != null)
                    {
                        objToken.TokenNo = Convert.ToInt32(objToken.TokenNo ?? 0) + 1;

                        TTokenNumberWithDoctorWise objCurrentToken = new()
                        {
                            TokenNo = objToken.TokenNo,
                            VisitDate = item.VisitDate,
                            VisitId = item.VisitId,
                            DoctorId = item.ConsultantDocId,
                            IsStatus = false
                        };
                        _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                        await _context.SaveChangesAsync();
                    }
                }

                scope.Complete();
            }

            //// OLD CODE With SP
            //DatabaseHelper odal = new();
            //string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            //var entity = objRegistration.ToDictionary();
            //foreach (var rProperty in rEntity)
            //{
            //    entity.Remove(rProperty);
            //}
            //string RegId = odal.ExecuteNonQuery("m_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            //objRegistration.RegId = Convert.ToInt32(RegId);
            //objVisitDetail.RegId = Convert.ToInt32(RegId);

            //string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            //var visitentity = objVisitDetail.ToDictionary();
            //foreach (var rProperty in rVisitEntity)
            //{
            //    visitentity.Remove(rProperty);
            //}
            //string VisitId = odal.ExecuteNonQuery("m_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            //objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            //var tokenObj = new
            //{
            //    PatVisitID = Convert.ToInt32(VisitId)
            //};
            //odal.ExecuteNonQuery("m_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }

        public virtual async Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            //// NEW CODE With EDMX
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update Registration table records
                _context.Registrations.Update(objRegistration);
                _context.Entry(objRegistration).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Add VisitDetail table records
                objVisitDetail.RegId = objRegistration.RegId;
                _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();

                // Update VisitDetail table records
                ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Add TokenNumber table records
                VisitDetail objVisit = await _context.VisitDetails.FirstOrDefaultAsync(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now);
                TTokenNumberWithDoctorWise objToken = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                objToken.TokenNo = Convert.ToInt32(objToken.TokenNo) + 1;

                // Add TokenNumber table records
                List<VisitDetail> objVisitList = await _context.VisitDetails.Where(x => x.VisitId == objVisitDetail.VisitId && x.VisitDate == DateTime.Now).ToListAsync();
                foreach (var item in objVisitList)
                {
                    TTokenNumberWithDoctorWise objTokenData = await _context.TTokenNumberWithDoctorWises.FirstOrDefaultAsync(x => x.VisitDate == DateTime.Now);
                    if (objTokenData != null)
                    {
                        objTokenData.TokenNo = Convert.ToInt32(objTokenData.TokenNo ?? 0) + 1;

                        TTokenNumberWithDoctorWise objCurrentToken = new()
                        {
                            TokenNo = objTokenData.TokenNo,
                            VisitDate = item.VisitDate,
                            VisitId = item.VisitId,
                            DoctorId = item.ConsultantDocId,
                            IsStatus = false
                        };
                        _context.TTokenNumberWithDoctorWises.Add(objCurrentToken);
                        await _context.SaveChangesAsync();
                    }
                }

                scope.Complete();
            }

            //// OLD CODE With SP
            //DatabaseHelper odal = new();
            //string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
            //var entity = objRegistration.ToDictionary();
            //foreach (var rProperty in rEntity)
            //{
            //    entity.Remove(rProperty);
            //}
            //string RegId = odal.ExecuteNonQuery("m_update_RegistrationForAppointment_1", CommandType.StoredProcedure, "RegId", entity);
            //objRegistration.RegId = Convert.ToInt32(RegId);
            //objVisitDetail.RegId = Convert.ToInt32(RegId);

            //string[] rVisitEntity = { "Opdno", "IsMark", "Comments", "IsXray" };
            //var visitentity = objVisitDetail.ToDictionary();
            //foreach (var rProperty in rVisitEntity)
            //{
            //    visitentity.Remove(rProperty);
            //}
            //string VisitId = odal.ExecuteNonQuery("m_insert_VisitDetails_1", CommandType.StoredProcedure, "VisitId", visitentity);
            //objVisitDetail.VisitId = Convert.ToInt32(VisitId);

            //var tokenObj = new
            //{
            //    PatVisitID = Convert.ToInt32(VisitId)
            //};
            //odal.ExecuteNonQuery("m_Insert_TokenNumber_DoctorWise", CommandType.StoredProcedure, tokenObj.ToDictionary());
        }
        public virtual async Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                VisitDetail objVisit = await _context.VisitDetails.FindAsync(objVisitDetail.VisitId);
                objVisit.IsCancelled = objVisitDetail.IsCancelled;
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
