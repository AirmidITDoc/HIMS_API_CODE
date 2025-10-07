using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
   public partial interface IBillCancellationService
    {
        Task UpdateAsyncOp(Bill objOpBillCancellation,  int CurrentUserId, string CurrentUserName);
        Task UpdateAsyncIp(Bill objIPBillCancellation,  int CurrentUserId, string CurrentUserName);
    }
}
