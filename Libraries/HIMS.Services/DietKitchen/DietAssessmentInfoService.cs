using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{
    public class DietAssessmentInfoService : IDietAssessmentInfoService
    {
        private readonly HIMSDbContext _context;

        public DietAssessmentInfoService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<DietAssessmentInfoDtoList>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietAssessmentInfoDtoList>(model, "ps_Rtrv_DietAssessmentInfo");
        }

        public virtual async Task<IPagedList<PatientAssessmentListDto>> PatientAssessmentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientAssessmentListDto>(model, "ps_PatientAssessment_list");
        }

        public virtual async Task Cancel(TDietAssessmentInfo ObjTDietAssessmentInfo, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                // 'IsCancelledDate' yithe add kela ahe
                string[] Entity = { "DietAssessmentId", "IsCancelledBy", "IsCancelledDate" };
                var entity = ObjTDietAssessmentInfo.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                // Current date and time set keli
                entity["IsCancelledDate"] = DateTime.Now;

                odal.ExecuteNonQueryNew("ps_DietAssessmentCancel", CommandType.StoredProcedure, "", entity);
                await _context.LogProcedureExecution(entity, nameof(TDietAssessmentInfo), (int)ObjTDietAssessmentInfo.DietAssessmentId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

       

        public virtual async Task<IPagedList<PatientvitalsdetailListDto>> DietGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientvitalsdetailListDto>(model, "ps_Patient_vitalsdetail");
        }

    }
}