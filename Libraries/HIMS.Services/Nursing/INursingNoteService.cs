using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial interface INursingNoteService
    {
        Task InsertAsyncSP(TNursingNote objTNursingNote, int currentUserId, string currentUserName);

        Task<IPagedList<NursingNoteListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<TDoctorPatientHandoverListDto>> SGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<DoctorsNoteListDto>> DoctorsNoteAsync(GridRequestModel objGrid);

        Task InsertAsync(TDoctorsNote ObjTDoctorsNote, int UserId, string Username);
        Task UpdateAsync(TDoctorsNote ObjTDoctorsNote, int UserId, string Username);
        Task InsertAsync(TDoctorPatientHandover ObjTDoctorPatientHandover, int UserId, string Username);

        Task UpdateAsync(TDoctorPatientHandover ObjTDoctorPatientHandover, int UserId, string Username);
        Task InsertAsync(TNursingPatientHandover ObjTNursingPatientHandover, int UserId, string Username);
        Task UpdateAsync(TNursingPatientHandover ObjTNursingPatientHandover, int UserId, string Username);
        Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username);
        Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel objGrid);
        Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel objGrid);
        Task<IPagedList<DoctorNoteListDto>> DoctorNoteList(GridRequestModel objGrid);
    }
}
