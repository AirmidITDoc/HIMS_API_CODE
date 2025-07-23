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
    public class OTBookingRequestService : IOTBookingRequestService
    {
        private readonly HIMSDbContext _context;
        public OTBookingRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OTBookingRequestListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OTBookingRequestListDto>(model, "m_rtrv_T_OTBooking_Request_List");
        }

        public virtual async Task InsertAsync(TOtbookingRequest objOTBooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TOtbookingRequests.Add(objOTBooking);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TOtbookingRequest objOTBooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TOtbookingRequests.Update(objOTBooking);
                _context.Entry(objOTBooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task CancelAsync(TOtbookingRequest OBJTOtbookingRequest, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] OEntity = { "OtbookingDate", "OtbookingTime", "OpIpId", "OpIpType", "SurgeryType", "SiteDescId", "SurgeryId", "SurgeonId", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy", "IsCancelledDateTime", "OtrequestDate", "OtrequestTime", "OtrequestId", "DepartmentId","CategoryId", "IsCancelled", "IsCancelledBy" };
            var TEntity = OBJTOtbookingRequest.ToDictionary();
            foreach (var rProperty in OEntity)
            {
                TEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("m_Cancel_T_OTBooking_Request", CommandType.StoredProcedure, TEntity);
        }

    }
}
