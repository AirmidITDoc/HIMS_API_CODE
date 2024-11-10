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

        Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username);

     }
} 
