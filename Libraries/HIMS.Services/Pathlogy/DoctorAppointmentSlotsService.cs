using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
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

namespace HIMS.Services.Pathlogy
{
    public class DoctorAppointmentSlotsService: IDoctorAppointmentSlotsService
    {
        private readonly HIMSDbContext _context;
        public DoctorAppointmentSlotsService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DoctorAppointmentSlotsListDto>> GetDoctorAppointmentSlotsListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorAppointmentSlotsListDto>(model, "ps_Get_DoctorAppointmentSlotsList");
        }
        public virtual async Task Insert(TDoctorAppointmentSlot Obj, int SlotDuration,int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); 
                odal.SetTransaction(transaction.GetDbTransaction());     

                string[] DEntity = { "DoctorId", "UnitId", "AppointmentDate", "StartTime", "EndTime", "SlotDuration", "SessionType" , "CreatedBy" };
                var dentity = Obj.ToDictionary();
                foreach (var rProperty in dentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        dentity.Remove(rProperty);

                }
                dentity["SlotDuration"] = SlotDuration;

                odal.ExecuteNonQueryNew("ps_Generate_DoctorAppointmentSlots", CommandType.StoredProcedure, "", dentity);
                await _context.LogProcedureExecution(dentity, nameof(TDoctorAppointmentSlot), Obj.DoctorId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task Cancel(TDoctorAppointmentSlot Obj, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());     

                string[] DEntity = { "SlotId", "CancelledBy"};
                var dentity = Obj.ToDictionary();
                foreach (var rProperty in dentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        dentity.Remove(rProperty);

                }
                dentity["CancelledBy"] = Obj.IsCancelledBy;
                odal.ExecuteNonQueryNew("ps_Cancel_DoctorAppointmentSlot", CommandType.StoredProcedure, "", dentity);
                await _context.LogProcedureExecution(dentity, nameof(TDoctorAppointmentSlot), Obj.DoctorId, Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
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
