using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public partial  interface IPhoneAppointmentService
    {


        Task InsertAsyncSP(TPhoneAppointment objPhoneAppointment, int UserId, string Username);
        Task InsertAsync(TPhoneAppointment objPhoneAppointment, int currentUserId, string currentUserName);


        Task CancelAsync(TPhoneAppointment objPhoneAppointment, int UserId, string Username);
    }

}

