using HIMS.Data.Models;


namespace HIMS.Services.Pharmacy
{
    public partial interface IGRNService
    {
        Task InsertAsync(TGrnheader user, List<MItemMaster> objItems, int UserId, string Username);
        Task InsertAsyncSP(TGrnheader user, List<MItemMaster> objItems, int UserId, string Username);
        Task UpdateAsync(TGrnheader user, List<MItemMaster> objItems, int UserId, string Username);
    }
}
