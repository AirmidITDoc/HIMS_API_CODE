using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IEstimasteService
    {
        Task InsertAsync(TEstimateHeader ObjTEstimateHeader, int UserId, string Username);
        //Task UpdateAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username, string[]? references);

        Task<IPagedList<EstimateListDto>> EstimateListAsync(GridRequestModel objGrid);

        Task<IPagedList<EstimateDetailsListDto>> EstimateDetailsListAsync(GridRequestModel objGrid);


        Task UpdateAsync(TEstimateHeader ObjTEstimateHeader, int UserId, string Username, string[]? references);
    }
}
