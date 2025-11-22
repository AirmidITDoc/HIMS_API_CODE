using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
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
        public virtual async Task<IPagedList<OTReservationListDto>> GetListOtReservationAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OTReservationListDto>(model, "ps_Rtrv_OT_ReservationList");
        }
        public virtual async Task<IPagedList<requestAttendentListDto>> OTGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<requestAttendentListDto>(model, "rtrv_reservationAttendentList");
        }
        public virtual async Task<IPagedList<ReservationSurgeryDetailListDto>> OTreservationGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ReservationSurgeryDetailListDto>(model, "rtrv_reservationsurgeryList");
        }
        public virtual async Task<List<ReservationDiagnosisListDto>> GetDiagnosisListAsync(string DescriptionType)
        {
            var query = _context.TOtReservationDiagnoses.AsQueryable();

            if (!string.IsNullOrEmpty(DescriptionType))
            {
                string lowered = DescriptionType.ToLower();
                query = query.Where(d => d.DescriptionType != null && d.DescriptionType.ToLower().Contains(lowered));
            }

            var data = await query
                .OrderBy(d => d.OtreservationDiagnosisDetId)
                .Select(d => new ReservationDiagnosisListDto
                {
                    OtreservationDiagnosisDetId = d.OtreservationDiagnosisDetId,
                    DescriptionType = d.DescriptionType,
                    DescriptionName = d.DescriptionName
                })
                .Take(50)
                .ToListAsync();

            return data;
        }

        public List<OTRequestDetailsListSearchDto> SearchPatient(string Keyword)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Keyword", Keyword);
            return sql.FetchListBySP<OTRequestDetailsListSearchDto>("ps_Rtrv_PatientOTRequestListSearch", para);
        }



        public virtual async Task InsertAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtReservationHeaders
                    .OrderByDescending(x => x.OtreservationNo)
                    .Select(x => x.OtreservationNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtReservationHeader.OtreservationNo = newSeqNo.ToString();


                ObjTOtReservationHeader.Createdby = UserId;
                ObjTOtReservationHeader.CreatedDate = DateTime.Now;

                _context.TOtReservationHeaders.Add(ObjTOtReservationHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task UpdateAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    {
        //        // Attach entity
        //        _context.Attach(ObjTOtReservationHeader);
        //        _context.Entry(ObjTOtReservationHeader).State = EntityState.Modified;

        //        // Prevent overwriting of important fields
        //        _context.Entry(ObjTOtReservationHeader).Property(x => x.Createdby).IsModified = false;
        //        _context.Entry(ObjTOtReservationHeader).Property(x => x.CreatedDate).IsModified = false;
        //        _context.Entry(ObjTOtReservationHeader).Property(x => x.OtreservationNo).IsModified = false;

        //        // Update modified fields
        //        ObjTOtReservationHeader.ModifiedBy = UserId;
        //        ObjTOtReservationHeader.ModifiedDate = DateTime.Now;

        //        // Ignore any additional columns if specified
        //        if (ignoreColumns?.Length > 0)
        //        {
        //            foreach (var column in ignoreColumns)
        //            {
        //                _context.Entry(ObjTOtReservationHeader).Property(column).IsModified = false;
        //            }
        //        }
        //        // Delete related detail records safely
        //        var lstSurgery = await _context.TOtReservationAttendingDetails.Where(x => x.OtreservationId == ObjTOtReservationHeader.OtreservationId).ToListAsync();
        //        if (lstSurgery.Count > 0)
        //            _context.TOtReservationAttendingDetails.RemoveRange(lstSurgery);

        //        var lstAttend = await _context.TOtReservationSurgeryDetails.Where(x => x.OtreservationId == ObjTOtReservationHeader.OtreservationId).ToListAsync();
        //        if (lstAttend.Count > 0)
        //            _context.TOtReservationSurgeryDetails.RemoveRange(lstAttend);

        //        var lstDiagnosis = await _context.TOtReservationDiagnoses
        //            .Where(x => x.OtreservationId == ObjTOtReservationHeader.OtreservationId)
        //            .ToListAsync();
        //        if (lstDiagnosis.Count > 0)
        //            _context.TOtReservationDiagnoses.RemoveRange(lstDiagnosis);



        //        await _context.SaveChangesAsync();
        //        scope.Complete();
        //    }
            public virtual async Task UpdateAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username, string[]? ignoreColumns = null)
            {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long reservationId = ObjTOtReservationHeader.OtreservationId;

                // ✅ Delete related details first
                var lstAttend = await _context.TOtReservationAttendingDetails
                    .Where(x => x.OtreservationId == reservationId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TOtReservationAttendingDetails.RemoveRange(lstAttend);

                var lstSurgery = await _context.TOtReservationSurgeryDetails
                    .Where(x => x.OtreservationId == reservationId)
                    .ToListAsync();
                if (lstSurgery.Any())
                    _context.TOtReservationSurgeryDetails.RemoveRange(lstSurgery);

                var lstDiagnosis = await _context.TOtReservationDiagnoses
                    .Where(x => x.OtreservationId == reservationId)
                    .ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TOtReservationDiagnoses.RemoveRange(lstDiagnosis);

                // ✅ Save deletion first
                await _context.SaveChangesAsync();

                // ✅ Then attach and update header
                _context.Attach(ObjTOtReservationHeader);
                _context.Entry(ObjTOtReservationHeader).State = EntityState.Modified;

                _context.Entry(ObjTOtReservationHeader).Property(x => x.Createdby).IsModified = false;
                _context.Entry(ObjTOtReservationHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTOtReservationHeader).Property(x => x.OtreservationNo).IsModified = false;

                ObjTOtReservationHeader.ModifiedBy = UserId;
                ObjTOtReservationHeader.ModifiedDate = DateTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTOtReservationHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
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
        //public virtual void InsertSP(TOtReservation ObjTOtReservation, int UserId, string UserName)
        //{

        //    DatabaseHelper odal = new();
        //    string[] Entity = { "OldOTReservationId", "OpIpId", "SurgeryDate", "CreatedBy", "Reason", "NewOTReservationId" };
        //    var entity = ObjTOtReservation.ToDictionary();
        //    foreach (var rProperty in entity.Keys.ToList())
        //    {
        //        if (!Entity.Contains(rProperty))
        //            entity.Remove(rProperty);
        //    }
        //    string VOtreservationId = odal.ExecuteNonQuery("ps_insert_T_OTReservation_PostPone", CommandType.StoredProcedure, "EmgId", entity);
        //    ObjTOtReservation.OtreservationId = Convert.ToInt32(VOtreservationId);
        //}
        public virtual void InsertSP(TOtReservation ObjTOtReservation, int UserId, string UserName)
        {
            DatabaseHelper odal = new();

            string[] Entity = { "OpIpId", "SurgeryDate", "CreatedBy", "Reason" };

            var entity = ObjTOtReservation.ToDictionary();

            // Keep only allowed columns
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            entity["OldOTReservationId"] = 0; // REQUIRED
            entity["NewOTReservationId"] = 0; // REQUIRED
            entity["CreatedBy"] = UserId;

            string VOtreservationId = odal.ExecuteNonQuery("ps_insert_T_OTReservation_PostPone", CommandType.StoredProcedure, "NewOTReservationId", entity);


            ObjTOtReservation.OtreservationId = Convert.ToInt32(VOtreservationId);
        }


        public virtual async Task InsertAsync(TOtReservationCheckInOut objTOtReservationCheckInOut, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtReservationCheckInOuts
                    .OrderByDescending(x => x.OtcheckInNo)
                    .Select(x => x.OtcheckInNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                objTOtReservationCheckInOut.OtcheckInNo = newSeqNo.ToString();


                objTOtReservationCheckInOut.CreatedBy = UserId;
                objTOtReservationCheckInOut.CreatedDate = DateTime.Now;

                _context.TOtReservationCheckInOuts.Add(objTOtReservationCheckInOut);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TOtReservationCheckInOut objTOtReservationCheckInOut, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                // Attach entity
                _context.Attach(objTOtReservationCheckInOut);
                _context.Entry(objTOtReservationCheckInOut).State = EntityState.Modified;

                // Prevent overwriting of important fields
                _context.Entry(objTOtReservationCheckInOut).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(objTOtReservationCheckInOut).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(objTOtReservationCheckInOut).Property(x => x.OtcheckInNo).IsModified = false;

                // Update modified fields
                objTOtReservationCheckInOut.ModifiedBy = UserId;
                objTOtReservationCheckInOut.ModifiedDate = DateTime.Now;

                // Ignore any additional columns if specified
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(objTOtReservationCheckInOut).Property(column).IsModified = false;
                    }
                }
              

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

    }
}
