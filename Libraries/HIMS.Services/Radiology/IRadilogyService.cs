using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Radiology
{
    public partial  interface IRadilogyService
    {
        Task<IPagedList<RadiologyListDto>> GetListAsync(GridRequestModel objGrid);
        void RadiologyUpdate(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username);
        Task UpdateAsync(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username);
        Task VerifyAsync(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username);


    }
}
