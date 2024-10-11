using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial interface IPriscriptionService
    {
        //Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
        Task InsertAsyncSP(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);

    }
}
