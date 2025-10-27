using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.OPPatient
{
    public class IPPrescriptionService : IIPrescriptionService
    {
        private readonly HIMSDbContext _context;
        public IPPrescriptionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PatietWiseMatetialListDto>> PatietWiseMatetialList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatietWiseMatetialListDto>(model, "Rtrv_PatMaterialConsumption_ByName");
        }
        public virtual async Task InsertAsyncSP(TPrescription objPrescription, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "ChiefComplaint", "IsAddBy", "SpO2", "DoseOption2", "DaysOption2", "DoseOption3", "DaysOption3" };
            var entity = objPrescription.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PrecriptionId = odal.ExecuteNonQuery("v_insert_Prescription_1", CommandType.StoredProcedure, "PrecriptionId", entity);
            objPrescription.PrecriptionId = Convert.ToInt32(PrecriptionId);

            await _context.SaveChangesAsync(UserId, Username);
        }


        public virtual async Task InsertAsyncSP(TPrescription objPrescription, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUsername)
        {
            //// NEW CODE With EDMX
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Add Registration table records
                _context.TPrescriptions.Add(objPrescription);
                await _context.SaveChangesAsync();

                // Add VisitDetail table records
                objVisitDetail.RegId = objPrescription.PrecriptionId;
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


        }
        public virtual async Task InsertAsync(TPrescription objPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPrescriptions.Add(objPrescription);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TPrescription objPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TPrescriptions.Update(objPrescription);
                _context.Entry(objPrescription).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
