using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IAdvanceService
    {
        Task InsertAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objpayment, int UserId, string UserName);
    }
}
