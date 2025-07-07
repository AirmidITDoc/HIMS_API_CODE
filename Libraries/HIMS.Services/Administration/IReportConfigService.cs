using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IReportConfigService
    {
        Task InsertAsyncm(MReportConfig ObjMReportConfig, int UserId, string Username);
        Task UpdateAsyncm(MReportConfig ObjMReportConfig, int UserId, string Username);
        Task<IPagedList<MReportConfigListDto>> MReportConfigList(GridRequestModel objGrid);


    }
}
