using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPharmacyReorderService
    {

        Task<IPagedList<ItemReorderListDto>> GetListAsync(GridRequestModel objGrid);



    }
}
