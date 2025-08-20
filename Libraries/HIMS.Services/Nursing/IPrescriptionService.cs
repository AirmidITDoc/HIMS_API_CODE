using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public partial  interface IPrescriptionService
    {
        Task InsertAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username);
        Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);

        Task PrescCancelAsync(TIpPrescription objTIpPrescription, int CurrentUserId, string CurrentUserName);
        Task PrescreturnCancelAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int CurrentUserId, string CurrentUserName);

    }
}
