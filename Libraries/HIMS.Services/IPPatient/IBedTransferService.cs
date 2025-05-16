using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial  interface IBedTransferService
    {
        Task<IPagedList<BedTransferDetailListDto>> BedTransferDetailList(GridRequestModel objGrid);

        Task InsertAsyncSP(TBedTransferDetail objBedTransferDetail, Bedmaster ObjBedMaster , Bedmaster ObjBedMasterUpdate, Admission ObjAdmission, int CurrentUserId, string CurrentUserName);

    }
}
