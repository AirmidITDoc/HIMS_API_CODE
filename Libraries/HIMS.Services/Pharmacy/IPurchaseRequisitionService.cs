using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPurchaseRequisitionService
    {
        Task InsertAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username);
        Task UpdateAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username, string[]? ignoreColumns = null);
        Task Cancel(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username);
        Task VerifyAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username);
        Task<IPagedList<PurchaseRequitionListDto>> GetListAsync(GridRequestModel objGrid);




    }
}
