using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPharmacyReorderService
    {
        
        Task<IPagedList<ItemReorderListDto>> GetListAsync(GridRequestModel objGrid);



    }
}
