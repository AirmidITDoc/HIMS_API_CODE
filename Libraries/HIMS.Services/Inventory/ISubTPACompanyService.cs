using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ISubTPACompanyService
    {
        Task<IPagedList<SubTpaCompanyListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
