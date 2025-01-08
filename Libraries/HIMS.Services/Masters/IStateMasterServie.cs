using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
   

    public partial interface IStateMasterServie
    {
        Task<IPagedList<MCityMaster>> GetAllPagedAsync(GridRequestModel objGrid);
    }
}
