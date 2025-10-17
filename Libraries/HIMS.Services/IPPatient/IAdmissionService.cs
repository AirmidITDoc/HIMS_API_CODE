using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IAdmissionService
    {
        Task<IPagedList<AdmissionListDto>> GetAdmissionListAsync(GridRequestModel objGrid);
        Task<IPagedList<AdmissionListDto>> GetAdmissionDischargeListAsync(GridRequestModel objGrid);
        Task InsertAsyncSP(Admission objAdmission, int CurrentUserId, string CurrentUserName);
        Task InsertRegAsyncSP(Registration ObjRegistration, Admission objAdmission, int CurrentUserId, string CurrentUserName);
        Task UpdateAdmissionAsyncSP(Admission objAdmission, int currentUserId, string currentUserName);
        Task<List<PatientAdmittedListSearchDto>> PatientAdmittedListSearch(string Keyword);
        Task<List<PatientAdmittedListSearchDto>> PatientDischargeListSearch(string Keyword);
        Task<PatientAdmittedListSearchDto> PatientByAdmissionId(long AdmissionId);
        Task UpdateAsyncInfo(Admission OBJAdmission, int UserId, string Username);
        Task<IPagedList<RequestForIPListDto>> GetAsync(GridRequestModel objGrid);
        Task<List<Bedmaster>> GetBedmaster(int RoomId);
        Task CancelAsync(Admission OBJAdmission, int UserId, string Username);
    }

}
