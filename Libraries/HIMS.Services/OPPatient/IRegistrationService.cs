using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface IRegistrationService
    {
        Task<IPagedList<RegistrationListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsyncSP(Registration objRegistration, int UserId, string Username);
        Task UpdateAsync(Registration objRegistration, int UserId, string Username);
        Task<List<RegistrationAutoCompleteDto>> SearchRegistration(string str);
    }
}

