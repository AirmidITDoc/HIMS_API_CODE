using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial  interface IIssueToDepService
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


    }
} 
