﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public virtual async Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestListDto>(model, "m_Rtrv_CanteenRequestDet");
        }
        public virtual async Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CanteenRequestHeaderListDto>(model, "m_Rtrv_CanteenRequestListFromWard");
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

       



        public virtual async Task InsertAsyncSP(TNursingNote objTNursingNote, int currentUserId, string currentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                //_context.TNursingNote.Add(objTNursingNote);
                //await _context.SaveChangesAsync();

                //scope.Complete();
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
    }
}
