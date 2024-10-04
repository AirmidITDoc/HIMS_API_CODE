using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPSettlementService
    {
        Task InsertAsyncSP(Payment objpayment, int CurrentUserId, string CurrentUserName);


    }
}
