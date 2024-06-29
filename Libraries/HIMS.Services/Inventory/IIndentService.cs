using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IIndentService
    {
        Task InsertAsync(TIndentHeader objIndent, int UserId, string Username);
        Task UpdateAsync(TIndentHeader objIndent, int UserId, string Username);
        Task VerifyAsync(TIndentHeader objIndent, int UserId, string Username);
    }
}
