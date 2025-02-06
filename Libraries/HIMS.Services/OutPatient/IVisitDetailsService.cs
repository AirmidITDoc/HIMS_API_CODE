using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IVisitDetailsService
    {
        Task<IPagedList<VisitDetailListDto>> GetListAsync(GridRequestModel objGrid);

        Task<IPagedList<OPBillListDto>> GetBillListAsync(GridRequestModel objGrid);

        Task<IPagedList<OPPaymentListDto>> GeOpPaymentListAsync(GridRequestModel objGrid);

        Task<IPagedList<OPRefundListDto>> GeOpRefundListAsync(GridRequestModel objGrid);

        Task<IPagedList<OPRegistrationList>> GeOPRgistrationListAsync(GridRequestModel objGrid);

        //Task<IPagedList<OPPhoneAppointmentList>> GeOPPhoneAppListAsync(GridRequestModel objGrid);

        

        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task InsertAsync(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);


        List<DeptDoctorListDoT> GetDoctor(int DepartmentId);


        Task<IPagedList<DeptDoctorListDoT>> GetListAsyncDoc(GridRequestModel objGrid);

    }
}
