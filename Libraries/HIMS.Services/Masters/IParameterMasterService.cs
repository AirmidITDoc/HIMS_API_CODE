using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IParameterMasterService
    {
        Task InsertAsync(MPathParameterMaster objPara, int UserId, string Username);
        //Task InsertAsyncSP(MPathParameterMaster objPara, int UserId, string Username);
        Task UpdateAsync(MPathParameterMaster objPara, int UserId, string Username);
        Task CancelAsync(MPathParameterMaster objPara, int UserId, string Username);
    }
}
