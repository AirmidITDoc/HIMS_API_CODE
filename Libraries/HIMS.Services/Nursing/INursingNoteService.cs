using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;

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

        Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username);
        Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel objGrid);
        Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel objGrid);
        Task<IPagedList<DoctorNoteListDto>> DoctorNoteList(GridRequestModel objGrid);
        Task UpdateAsync(TNursingPatientHandover model, int currentUserId, string currentUserName);
        Task InsertAsync(TNursingPatientHandover model, int currentUserId, string currentUserName);
        Task<IPagedList<MedicationChartListDto>> MedicationChartlist(GridRequestModel objGrid);
        Task<IPagedList<NursingPatientHandoverListDto>> NursingPatientHandoverList(GridRequestModel objGrid);
        Task<IPagedList<NursingMedicationListDto>> NursingMedicationlist(GridRequestModel objGrid);

        void Insert(List<TNursingMedicationChart> ObjTNursingMedicationChart, int currentUserId, string currentUserName);

        //Task InsertAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username);
        //Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
    }
}
