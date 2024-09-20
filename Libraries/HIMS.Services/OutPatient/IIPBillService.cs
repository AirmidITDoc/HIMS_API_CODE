using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IIPBillService
    {
        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);

    }
}
