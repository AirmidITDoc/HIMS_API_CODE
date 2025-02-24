using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Common
{
    public partial interface ICommonService
    {
        dynamic GetDDLByIdWithProc(DDLRequestModel model);
        dynamic GetDataSetByProc(ListRequestModel model);
        List<T> GetSingleListByProc<T>(ListRequestModel model);

        Task<IPagedList<dynamic>> CommonList(GridRequestModel objGrid, string SP_Name);
    }
}
