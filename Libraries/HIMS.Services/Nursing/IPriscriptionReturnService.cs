using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial interface IPriscriptionReturnService
    {
        Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
        Task UpdateAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
        Task<IPagedList<PrescriptionReturnDto>> GetListAsync(GridRequestModel objGrid);

        Task<IPagedList<PrescriptionListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionReturnListDto>> GetListAsyncReturn(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionDetailListDto>> GetListAsyncDetail(GridRequestModel objGrid);

    }
}
