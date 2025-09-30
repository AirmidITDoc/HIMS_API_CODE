using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IIndentService
    {

        Task<IPagedList<IndentListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IndentListbyIdDto>> GetListAsyncs(GridRequestModel objGrid);
        Task<IPagedList<IndentItemListDto>> GetIndentItemListAsync(GridRequestModel objGrid);
        Task InsertAsync(TIndentHeader objIndent, int UserId, string Username);
        Task InsertAsyncSP(TIndentHeader objIndent, int UserId, string Username);
        Task UpdateAsync(TIndentHeader objIndent, int UserId, string Username, string[]? references);
        Task VerifyAsync(TIndentHeader objIndent, int UserId, string Username);
        Task CancelAsync(TIndentHeader objIndent, int UserId, string Username);
        Task<IPagedList<IndentItemListDto>> GetOldIndentAsync(GridRequestModel objGrid);


    }
}
