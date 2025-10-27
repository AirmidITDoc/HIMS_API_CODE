using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public class OTService : IOTService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OTService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public async Task<TOtReservation?> GetByIdAsync(int id)
        {
            return await _context.TOtReservations.FirstOrDefaultAsync(x => x.OtreservationId == id);
        }
        public virtual async Task<IPagedList<OTBookinglistDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OTBookinglistDto>(model, "ps_Rtrv_OTBookinglist");
        }

        public virtual async Task InsertAsync(TOtReservation OBJTOtbooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TOtReservations.Add(OBJTOtbooking);

                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task InsertAsync(TOtReservation OBJTOtbooking, TOtbookingRequest objTOtbookingRequests, int UserId, string Username)
        //{
        //     using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //     _context.TOtReservations.Add(OBJTOtbooking);
        //    await _context.SaveChangesAsync();   

        //    var existing = await _context.TOtbookingRequests .FirstOrDefaultAsync(x => x.OtbookingId == objTOtbookingRequests.OtrequestId);

        //    if (existing != null) 
        //    {
        //        existing.OtrequestId = objTOtbookingRequests.OtrequestId;

        //        _context.Entry(existing).Property(x => x.OtrequestId).IsModified = true;
        //        await _context.SaveChangesAsync();
        //    }

        //    scope.Complete();
        //}
        public virtual async Task UpdateAsync(TOtReservation OBJTOtbooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TOtReservations.Update(OBJTOtbooking);
                _context.Entry(OBJTOtbooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual void Cancel(TOtReservation objTOtReservation, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "ReservationDate", "ReservationTime", "OpIpId", "OpIpType", "Opdate", "OpstartTime", "OpendTime", "Duration", "OttableId", "SurgeonId", "SurgeonId1", "AnestheticsDr", "AnestheticsDr1", "SurgeryId", "AnesthTypeId", "Instruction", "OttypeId", "UnBooking", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy", "IsCancelledDateTime", "IsCancelled", "DepartmentId", "OtrequestId" };
            var CAdvanceEntity = objTOtReservation.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                CAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_Cancel_T_OTBooking", CommandType.StoredProcedure, CAdvanceEntity);
        }
        public virtual void InsertSP(TOtReservation ObjTOtReservation, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] Entity = { "ReservationDate", "ReservationTime", "OpIpType", "OpstartTime", "OpendTime", "Duration", "OttableId", "DepartmentId", "SurgeonId", "SurgeonId1", "AnestheticsDr", "AnestheticsDr1", "SurgeryId", "AnesthTypeId", "Instruction", "OttypeId", "UnBooking", "IsCancelled", "IsCancelledBy", "IsCancelledDateTime", "CreatedDate", "ModifiedDate", "ModifiedBy", "OtrequestId" };
            var entity = ObjTOtReservation.ToDictionary();
            foreach (var rProperty in Entity)
            {
                entity.Remove(rProperty);
            }

            string VOtreservationId = odal.ExecuteNonQuery("ps_insert_T_OTBooking_PostPone", CommandType.StoredProcedure, "EmgId", entity);
            ObjTOtReservation.OtreservationId = Convert.ToInt32(VOtreservationId);
        }


    }
}
