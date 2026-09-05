using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public  partial interface IClassMasterService
    {
        Task InsertAsync(ServiceDetail ObjServiceDetail, int CurrentUserId, string CurrentUserName, int OldClassId, int NewClassId);
    }
}
