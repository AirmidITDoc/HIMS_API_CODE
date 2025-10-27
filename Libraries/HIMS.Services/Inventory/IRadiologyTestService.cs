using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IRadiologyTestService
    {
        Task<IPagedList<RadiologyPatientListDto>> GetListAsyn(GridRequestModel objGrid);
        Task<IPagedList<RadTemplateMasterListDto>> TemplateListAsyn(GridRequestModel objGrid);
        Task InsertAsyncSP(MRadiologyTestMaster objRadio, int UserId, string Username);
        Task InsertAsync(MRadiologyTestMaster objRadio, int UserId, string Username);
        Task CancelAsync(MRadiologyTestMaster objRadio, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(MRadiologyTestMaster objRadio, int UserId, string Username, string[]? references);
        Task<List<MRadiologyTestMaster>> GetAllRadiologyTest();
        Task<IPagedList<RadiologyTestListDto>> RadiologyTestList(GridRequestModel objGrid);
    }
}
