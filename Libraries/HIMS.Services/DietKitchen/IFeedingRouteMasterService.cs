using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IFeedingRouteMasterService
    {
        Task InsertAsync(MFeedingRouteMaster ObjMFeedingRouteMaster, int UserId, string Username);
        Task UpdateAsync(MFeedingRouteMaster ObjMFeedingRouteMaster, int UserId, string Username, string[]? ignoreColumns = null);

    }
}

