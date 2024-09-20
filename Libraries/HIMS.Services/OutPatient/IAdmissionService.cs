using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IAdmissionService
    {
        Task InsertAsyncSP(Registration objRegistration, Admission objAdmission, int currentUserId, string currentUserName);
        Task InsertRegAsyncSP(Admission objAdmission, int currentUserId, string currentUserName);

    }
}
