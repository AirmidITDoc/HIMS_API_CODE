using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IBedTransferService
    {
        Task<IPagedList<BedTransferDetailListDto>> BedTransferDetailList(GridRequestModel objGrid);

        void InsertSP(TBedTransferDetail objBedTransferDetail, Bedmaster ObjBedMaster, Bedmaster ObjBedMasterUpdate, Admission ObjAdmission, int CurrentUserId, string CurrentUserName);

    }
}
