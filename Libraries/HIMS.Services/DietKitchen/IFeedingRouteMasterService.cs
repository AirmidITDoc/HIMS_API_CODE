using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietkitchenMaster
{
    public partial interface IFeedingRouteMasterService
    {
        Task InsertAsync(MFeedingRouteMaster ObjMFeedingRouteMaster, int UserId, string Username);
        Task UpdateAsync(MFeedingRouteMaster ObjMFeedingRouteMaster, int UserId, string Username, string[]? ignoreColumns = null);
        Task<IPagedList<FeedingRouteListDto>> GetFeedingRouteListAsync(GridRequestModel model);

    }
}

