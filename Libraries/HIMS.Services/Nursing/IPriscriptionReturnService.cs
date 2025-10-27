using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public partial interface IPriscriptionReturnService
    {
        //Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
        Task UpdateAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
        Task<IPagedList<PrescriptionReturnDto>> GetListAsync(GridRequestModel objGrid);

        Task<IPagedList<PrescriptionListDto>> GetPrescriptionListAsync(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionReturnListDto>> GetListAsyncReturn(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionDetailListDto>> GetListAsyncDetail(GridRequestModel objGrid);
        Task InsertAsync(TIpPrescription ObjTIpPrescription, int UserId, string Username);


    }
}
