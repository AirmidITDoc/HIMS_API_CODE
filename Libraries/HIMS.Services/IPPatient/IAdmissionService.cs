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
        void InsertAsyncSP(Admission objAdmission, int currentUserId, string currentUserName);
        Task InsertRegAsyncSP(Admission objAdmission, Bedmaster ObjBedmaster, int currentUserId, string currentUserName);
        Task UpdateAdmissionAsyncSP(Admission objAdmission, int currentUserId, string currentUserName);
        Task<List<PatientAdmittedListSearchDto>> PatientAdmittedListSearch(string Keyword);
        Task<List<PatientAdmittedListSearchDto>> PatientDischargeListSearch(string Keyword);
        Task<PatientAdmittedListSearchDto> PatientByAdmissionId(long AdmissionId);
        Task UpdateAsyncInfo(Admission OBJAdmission, int UserId, string Username);
        Task<IPagedList<RequestForIPListDto>> GetAsync(GridRequestModel objGrid);



    }
}
