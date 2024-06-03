using HIMS.Core.Domain.Grid;
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
        dynamic GetDataSetByProc(string mode, ListRequestModel model);
    }
}
