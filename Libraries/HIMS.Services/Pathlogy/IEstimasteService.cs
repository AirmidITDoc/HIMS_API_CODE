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
    }
}
