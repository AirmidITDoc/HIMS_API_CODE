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
    public partial interface ISupplierService
    {
        Task<IPagedList<SupplierListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsyncSP(MSupplierMaster objSupplier, int UserId, string Username);
        Task InsertAsync(MSupplierMaster objSupplier, int UserId, string Username);
        Task UpdateAsync(MSupplierMaster objSupplier, int UserId, string Username);
        Task CancelAsync(MSupplierMaster objSupplier, int UserId, string Username);
        Task<MSupplierMaster> GetById(int Id);

    }
}
