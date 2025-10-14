using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IBarcodeConfigService
    {
        Task<IPagedList<DbPrefixMaster>> GetAllPagedAsync(GridRequestModel objGrid);
    }
}
