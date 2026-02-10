using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface ITestMasterServices
    {
        Task<IPagedList<PathTestListDto>> PetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PathTestDetailDto>> PathTestDetailListAsync(GridRequestModel objGrid);
        Task<IPagedList<PathTestForUpdateListdto>> ListAsync(GridRequestModel objGrid);
        Task<IPagedList<SubTestMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateList(GridRequestModel objGrid);
        Task InsertAsync(MPathTestMaster objTest, int UserId, string Username);
        void InsertSP(MPathTestMaster objTest, List<MPathTemplateDetail> ObjMPathTemplateDetail, List<MPathTestDetailMaster> ObjMPathTestDetailMaster, int UserId, string Username);
        void UpdateSP(MPathTestMaster objTest, List<MPathTemplateDetail> ObjMPathTemplateDetail, List<MPathTestDetailMaster> ObjMPathTestDetailMaster, int UserId, string Username);
        Task UpdateAsync(MPathTestMaster objTest, int UserId, string Username);
        Task PaymentDateTimeUpdate(TPaymentPharmacy ObjTPaymentPharmacy, int UserId, string Username);
        Task TestUpdateAsync(MPathTestMaster ObjMPathTestMaster, int UserId, string Username);




    }
}
