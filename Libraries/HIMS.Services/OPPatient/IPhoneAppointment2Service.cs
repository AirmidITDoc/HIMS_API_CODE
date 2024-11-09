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
        //Task CancelAsync(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<PhoneAppointment2ListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
