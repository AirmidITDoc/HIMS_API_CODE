using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface IApprovalService
    {
        Task<IPagedList<ApprovalListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TApprovalHeader ObjTApprovalHeader, int UserId, string Username);
        Task<IPagedList<UserApprovalNamelistDto>> NewGetListAsync(GridRequestModel objGrid);
        Task UpdateAsync(TApprovalHeader objApproval, int currentUserId, string currentUserName);




    }
}
