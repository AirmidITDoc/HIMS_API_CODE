using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IVisitDetailsService
    {
        Task<IPagedList<VisitDetailListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<OPBillListDto>> GetBillListAsync(GridRequestModel objGrid);
        Task<IPagedList<OPPaymentListDto>> GeOpPaymentListAsync(GridRequestModel objGrid);
        Task<IPagedList<OPPaymentListDto>> GetPatientWisePaymentList(GridRequestModel objGrid);
        Task<IPagedList<OPRefundListDto>> GeOpRefundListAsync(GridRequestModel objGrid);
        Task<IPagedList<OPRegistrationList>> GeOPRgistrationListAsync(GridRequestModel objGrid);
        Task<IPagedList<PrevDrVisistListDto>> GeOPPreviousDrVisitListAsync(GridRequestModel objGrid);
        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, TPatientPolicyInformation ObjTPatientPolicyInformation, int currentUserId, string currentUserName);
        Task InsertAsync(Registration objRegistration, VisitDetail objVisitDetail,  int currentUserId, string currentUserName);
        Task UpdateAsyncSP(VisitDetail objVisitDetail, TPatientPolicyInformation ObjTPatientPolicyInformation, int currentUserId, string currentUserName);
        Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
        List<DeptDoctorListDoT> GetDoctor(int DepartmentId);
        Task<IPagedList<DeptDoctorListDoT>> GetListAsyncDoc(GridRequestModel objGrid);
        Task<List<ServiceMasterDTO>> GetServiceListwithTraiff(int TariffId, int ClassId, string ServiceName);
        Task<List<VisitDetailsListSearchDto>> VisitDetailsListSearchDto(string Keyword);
        Task<VisitDetailsListSearchDto> PatientByVisitId(long VisitId);
        Task InsertAsyncSP(VisitDetail objCrossConsultation, int UserId, string Username);
        void UpdateVital(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(VisitDetail ObjVisitDetail, int UserId, string Username);
        Task UpdateAsyncv(VisitDetail ObjVisitDetail, int UserId, string Username);
        void RequestForOPTOIP(VisitDetail ObjVisitDetail, int UserId, string Username);
        List<VisitDetailsListSearchDto> SearchPatient(string Keyword);
        Task ConsultantDoctorUpdate(VisitDetail objVisitDetail, int UserId, string Username);
        Task VistDateTimeUpdateAsync(VisitDetail ObjVisitDetail, int UserId, string Username);
        List<ServiceMasterDTO> SearchGetServiceListwithTraiff(int TariffId, int ClassId, string SrvcName);

    }
}
