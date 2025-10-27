using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class NursingNoteService : INursingNoteService
    {
        private readonly HIMSDbContext _context;
        public NursingNoteService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<NursingNoteListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingNoteListDto>(model, "m_Rtrv_NursingNotesList");
        }
        public virtual async Task<IPagedList<TDoctorPatientHandoverListDto>> SGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TDoctorPatientHandoverListDto>(model, "m_Rtrv_T_Doctor_PatientHandoverList");
        }
        public virtual async Task<IPagedList<DoctorsNoteListDto>> DoctorsNoteAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorsNoteListDto>(model, "Rtrv_DoctorsNotesList");
        }
        public virtual async Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestListDto>(model, "m_Rtrv_CanteenRequestDet");
        }
        public virtual async Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestHeaderListDto>(model, "m_Rtrv_CanteenRequestListFromWard");
        }
        public virtual async Task<IPagedList<MedicationChartListDto>> MedicationChartlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MedicationChartListDto>(model, "m_rtrvNursingDrugListForMedication");
        }
        public virtual async Task<IPagedList<NursingPatientHandoverListDto>> NursingPatientHandoverList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingPatientHandoverListDto>(model, "m_Rtrv_T_Nursing_PatientHandoverList");
        }
        public virtual async Task<IPagedList<NursingMedicationListDto>> NursingMedicationlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingMedicationListDto>(model, "m_Rtrv_NursingMedication_Scheduler_list");
        }

        //Ashu//
        public virtual async Task InsertAsync(TDoctorsNote ObjTDoctorsNote, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TDoctorsNotes.Add(ObjTDoctorsNote);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TDoctorsNote ObjTDoctorsNote, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TDoctorsNotes.Update(ObjTDoctorsNote);
                _context.Entry(ObjTDoctorsNote).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task InsertAsync(TDoctorPatientHandover ObjTDoctorPatientHandover, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TDoctorPatientHandovers.Add(ObjTDoctorPatientHandover);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TDoctorPatientHandover ObjTDoctorPatientHandover, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TDoctorPatientHandovers.Update(ObjTDoctorPatientHandover);
                _context.Entry(ObjTDoctorPatientHandover).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task InsertAsync(TNursingPatientHandover ObjTNursingPatientHandover, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TNursingPatientHandovers.Add(ObjTNursingPatientHandover);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TNursingPatientHandover ObjTNursingPatientHandover, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TNursingPatientHandovers.Update(ObjTNursingPatientHandover);
                _context.Entry(ObjTNursingPatientHandover).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }



        public virtual async Task InsertAsyncSP(TNursingNote objTNursingNote, int currentUserId, string currentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TNursingNotes.Add(objTNursingNote);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TCanteenRequestHeaders.Add(objCanteen);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public Task<IPagedList<DoctorNoteListDto>> DoctorNoteList(GridRequestModel objGrid)
        {
            throw new NotImplementedException();
        }

        //public virtual async Task InsertAsync(TNursingMedicationChart1 ObjTNursingMedicationChart1, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TNursingMedicationCharts1.Add(ObjTNursingMedicationChart1);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

        public virtual void Insert(List<TNursingMedicationChart> ObjTNursingMedicationChart, int UserId, string UserName)
        {

            DatabaseHelper odal = new();


            foreach (var item in ObjTNursingMedicationChart)
            {
                string[] rEntity = { "IsCancelledBy", "IsCancelledDateTime", "CreatedBy", "CreatedDatetime", "ModifiedBy", "ModifiedDateTime" };

                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_insert_T_Nursing_MedicationChart", CommandType.StoredProcedure, entity);
                // ObjTNursingMedicationChart.MedChartId = Convert.ToInt32(AMedChartId);
            }
        }


        //public virtual async Task InsertAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TIpmedicalRecords.Add(objmedicalRecord);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}


        //public virtual async Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TIpprescriptionReturnHs.Add(objIpprescriptionReturnH);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}




    }
}
