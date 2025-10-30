using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
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
        public virtual async Task<IPagedList<OTBookingRequestEmergencyListDto>> GetListAsynco(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OTBookingRequestEmergencyListDto>(model, "m_Rtrv_OTBookingRequestlist_EmergencyList");
        }

        public virtual void Cancel(TOtbookingRequest OBJOtbookingRequest, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] BEntity = { "OtbookingDate", "OtbookingTime", "OpIpId", "OpIpType", "OtrequestDate", "OtrequestTime", "OtrequestId", "SurgeryCategoryId", "DepartmentId", "CategoryId", "SiteDescId", "SurgeryId", "SurgeonId", "SurgeryTypeId", "CreatedBy", "CreatedDate", "ModifiedDate", "ModifiedBy", "IsCancelled", "IsCancelledDateTime" };
            var TEntity = OBJOtbookingRequest.ToDictionary();
            foreach (var rProperty in BEntity)
            {
                TEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_Cancel_T_OTBooking_Request", CommandType.StoredProcedure, TEntity);
        }

        public virtual async Task InsertAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtRequestHeaders
                    .OrderByDescending(x => x.OtrequestNo)
                    .Select(x => x.OtrequestNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtRequestHeader.OtrequestNo = newSeqNo.ToString();


                ObjTOtRequestHeader.CreatedBy = UserId;
                ObjTOtRequestHeader.CreatedDate = DateTime.Now;

                _context.TOtRequestHeaders.Add(ObjTOtRequestHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {


                    // Delete details table realted records
                    var lst = await _context.TOtRequestSurgeryDetails.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
                    if (lst.Count > 0)
                    {
                        _context.TOtRequestSurgeryDetails.RemoveRange(lst);
                    }
                    // Delete details table realted records
                    var lsts = await _context.TOtRequestAttendingDetails.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
                    if (lst.Count > 0)
                    {
                        _context.TOtRequestAttendingDetails.RemoveRange(lsts);
                    }
                    // Delete details table realted records
                    var lstd = await _context.TOtRequestDiagnoses.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
                    if (lst.Count > 0)
                    {
                        _context.TOtRequestDiagnoses.RemoveRange(lstd);
                    }

                     await _context.SaveChangesAsync();
                    // Update header & detail table records
                    _context.TOtRequestHeaders.Update(ObjTOtRequestHeader);
                    _context.Entry(ObjTOtRequestHeader).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    scope.Complete();
                }
            }
        }
    }

   


//        public virtual async Task UpdateAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username, string[]? ignoreColumns = null)
    //    {
    //        using var scope = new TransactionScope(TransactionScopeOption.Required,
    //            new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
    //            TransactionScopeAsyncFlowOption.Enabled);

    //        // Fetch existing header record (EF will track it)
    //        var existing = await _context.TOtRequestHeaders
    //            .Include(x => x.TOtRequestSurgeryDetails)
    //            .Include(x => x.TOtRequestAttendingDetails)
    //            .Include(x => x.TOtRequestDiagnoses)
    //            .FirstOrDefaultAsync(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId);

    //        if (existing == null)
    //            throw new Exception("Record not found.");

    //        // Preserve fields you don't want overwritten
    //        ObjTOtRequestHeader.OtrequestNo = existing.OtrequestNo;

    //        // Delete existing child records
    //        if (existing.TOtRequestSurgeryDetails?.Count > 0)
    //            _context.TOtRequestSurgeryDetails.RemoveRange(existing.TOtRequestSurgeryDetails);

    //        if (existing.TOtRequestAttendingDetails?.Count > 0)
    //            _context.TOtRequestAttendingDetails.RemoveRange(existing.TOtRequestAttendingDetails);

    //        if (existing.TOtRequestDiagnoses?.Count > 0)
    //            _context.TOtRequestDiagnoses.RemoveRange(existing.TOtRequestDiagnoses);

    //        await _context.SaveChangesAsync();

    //        // Apply new values from incoming object to existing header
    //        _context.Entry(existing).CurrentValues.SetValues(ObjTOtRequestHeader);

    //        // Ignore specific columns if requested
    //        if (ignoreColumns?.Length > 0)
    //        {
    //            foreach (var column in ignoreColumns)
    //            {
    //                _context.Entry(existing).Property(column).IsModified = false;
    //            }
    //        }

    //        // Add new detail records
    //        if (ObjTOtRequestHeader.TOtRequestSurgeryDetails?.Count > 0)
    //            _context.TOtRequestSurgeryDetails.AddRange(ObjTOtRequestHeader.TOtRequestSurgeryDetails);

    //        if (ObjTOtRequestHeader.TOtRequestAttendingDetails?.Count > 0)
    //            _context.TOtRequestAttendingDetails.AddRange(ObjTOtRequestHeader.TOtRequestAttendingDetails);

    //        if (ObjTOtRequestHeader.TOtRequestDiagnoses?.Count > 0)
    //            _context.TOtRequestDiagnoses.AddRange(ObjTOtRequestHeader.TOtRequestDiagnoses);

    //        await _context.SaveChangesAsync();
    //        scope.Complete();
    //    }

    //}

