using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
namespace HIMS.Services.Nursing
{
    public partial interface INursingConsentService
    {

        Task<IPagedList<ConsentDeptListDto>> GetListAsync(GridRequestModel objGrid);
        void Insert(TConsentInformation ObjTConsentInformation, int UserId, string Username);
        void Update(TConsentInformation ObjTConsentInformation, int UserId, string Username);
        Task<IPagedList<ConsentpatientInfoListDto>> ConsentpatientInfoList(GridRequestModel objGrid);
        Task<List<MConsentMaster>> GetConsent(int deptId, string? consentType);





    }
}
