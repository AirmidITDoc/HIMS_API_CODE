using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IIssueToDepService
    {

        Task<IPagedList<IssuetodeptListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IssueToDepartmentDetailListDto>> GetIssueItemListAsync(GridRequestModel objGrid);
        Task<IPagedList<IndentByIDListDto>> GetIndentById(GridRequestModel objGrid);
        Task<IPagedList<IndentItemListDto>> GetIndentItemList(GridRequestModel objGrid);
        Task<IPagedList<AcceptIssueItemDetListDto>> AcceptIssueItemDetList(GridRequestModel objGrid);
        Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, List<TCurrentStock> OBjTCurrentStock, int UserId, string Username);
        Task<IPagedList<MateralreceivedbyDeptLstDto>> GetMaterialrecivedbydeptList(GridRequestModel objGrid);
        Task<IPagedList<MaterialrecvedbydepttemdetailslistDto>> GetRecceivedItemListAsync(GridRequestModel objGrid);
        Task InsertAsync(TIssueToDepartmentHeader objIssueToDeptIndent, int UserId, string Username);
        Task UpdateSP(TIssueToDepartmentHeader ObjTIssueToDepartmentHeader, List<TCurrentStock> OBjCurrentStock, TIndentHeader ObjTIndentHeader, List<TIndentDetail> ObjTIndentDetail, int UserId, string Username);
        void Update(TIssueToDepartmentHeader ObjTIssueToDepartmentHeader, List<TIssueToDepartmentDetail> ObjTIssueToDepartmentDetail, TCurrentStock ObjTCurrentStock, int UserId, string Username);
        Task VerifyAsync(TIssueToDepartmentHeader ObjTIssueToDepartmentHeader, int UserId, string Username);
        Task InsertMaterialAsync(TIssueToDepartmentHeader objIssueToDeptIndent,  List<TIssueToDepartmentDetail> ObjTIssueToDepDetail, List<TIssueToDepartmentDetail> ObjTIssueToDepartmentDetail, List<TCurrentStock> OBjTCurrentStock, TIssueToDepartmentHeader ObjIssueDepartment, int UserId, string Username);
        Task UpdateIndentMaterialAccept(TIssueToDepartmentHeader objIssueToDeptIndent, List<TIssueToDepartmentDetail> ObjTIssueToDepDetail, List<TIssueToDepartmentDetail> ObjTIssueToDepartmentDetail, List<TCurrentStock> OBjTCurrentStock, TIssueToDepartmentHeader ObjIssueDepartment, TIndentHeader ObjTIndentHeader, List<TIndentDetail> ObjTIndentDetail, int UserId, string Username);






    }
}
