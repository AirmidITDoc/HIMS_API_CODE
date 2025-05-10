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

        Task<IPagedList<TIssueToDepartmentDetail>> GetIssueItemListAsync(GridRequestModel objGrid);

        Task<IPagedList<IndentByIDListDto>> GetIndentById(GridRequestModel objGrid);

        Task<IPagedList<IndentItemListDto>> GetIndentItemList(GridRequestModel objGrid);
       

        Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, TCurrentStock OBjTCurrentStock, int UserId, string Username);

     }
} 
