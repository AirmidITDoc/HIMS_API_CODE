using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public  class OTService : IOTService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OTService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OTBookinglistDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OTBookinglistDto>(model, "A_Rtrv_OTBookinglist");
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
        public virtual async Task CancelAsync(TOtReservation objTOtReservation, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "ReservationDate", "ReservationTime", "OpIpId", "OpIpType", "Opdate", "OpstartTime", "OpendTime", "Duration", "OttableId", "SurgeonId", "SurgeonId1", "AnestheticsDr", "AnestheticsDr1", "SurgeryId", "AnesthTypeId", "Instruction", "OttypeId", "UnBooking", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy", "IsCancelledDateTime","IsCancelled", "IsCancelledBy" };
            var CAdvanceEntity = objTOtReservation.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                CAdvanceEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Cancel_T_OTBooking", CommandType.StoredProcedure, CAdvanceEntity);
        }


    }
}
