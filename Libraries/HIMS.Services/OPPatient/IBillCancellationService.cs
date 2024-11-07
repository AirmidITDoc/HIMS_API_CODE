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
        Task UpdateAsyncOp(Bill objOpBillCancellation, int UserId, string Username);
        Task UpdateAsyncIp(Bill objIPBillCancellation, int UserId, string Username);
    }
}
