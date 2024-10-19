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
        Task InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int UserId, string Username);

    }
}
