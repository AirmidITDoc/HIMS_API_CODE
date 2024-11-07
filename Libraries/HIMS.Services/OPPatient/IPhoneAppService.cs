using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public partial interface IPhoneAppService
    {
        Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName);
        Task CancelAsync(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName);



    }
}
