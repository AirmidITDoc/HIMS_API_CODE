using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial interface IIPLabRequestService
    {
        //TDlabRequest objTDlabRequest,
        Task InsertAsyncSP(THlabRequest objTHlabRequest, int currentUserId, string currentUserName);
    }
}
