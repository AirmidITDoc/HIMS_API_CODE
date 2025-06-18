using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public partial interface IPhoneAppointment2Service
    {
        Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName);
        Task InsertAsync(TPhoneAppointment objTPhoneAppointment, int UserId, string Username);
        Task CancelAsync(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<PhoneAppointment2ListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<TPhoneAppointment>> GetPhoneListAsync(GridRequestModel objGrid);
        Task<List<PhoneAutoCompleteDto>> SearchPhoneApp(string str);
        Task<IPagedList<FutureAppointmentDetailListDto>> GetListAsyncF(GridRequestModel objGrid);
        Task<IPagedList<FutureAppointmentListDto>> FutureAppointmentList(GridRequestModel objGrid);
        Task<List<TPhoneAppointment>> GetAppoinments(int DocId, DateTime FromDate, DateTime ToDate);



    }
}
