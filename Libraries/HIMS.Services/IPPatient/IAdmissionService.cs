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
        //Task<IPagedList<AdvanceListDto>> GetAdvanceListAsync(GridRequestModel objGrid);
        //Task<IPagedList<RefundOfAdvanceListDto>> GetRefundOfAdvanceListAsync(GridRequestModel objGrid);
        //Task<IPagedList<IPBillListDto>> GetIPBillListListAsync(GridRequestModel objGrid);
        //Task<IPagedList<IPPaymentListDto>> GetIPPaymentListAsync(GridRequestModel objGrid);
        //Task<IPagedList<IPRefundBillListDto>> GetIPRefundBillListListAsync(GridRequestModel objGrid);
        Task InsertAsyncSP(Registration objRegistration, Admission objAdmission, int currentUserId, string currentUserName);
        Task InsertRegAsyncSP(Admission objAdmission, int currentUserId, string currentUserName);
        Task UpdateAdmissionAsyncSP(Admission objAdmission, int currentUserId, string currentUserName);
        Task<List<PatientAdmittedListSearchDto>> PatientAdmittedListSearch(string Keyword);
    }
}
