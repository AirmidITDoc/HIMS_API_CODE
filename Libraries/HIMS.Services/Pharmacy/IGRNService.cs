using HIMS.Data.Models;


namespace HIMS.Services.Pharmacy
{
    public partial interface IGRNService
    {
        Task InsertAsync(TGrnheader objGRN, List<MItemMaster> objItems, int UserId, string Username);
        Task InsertAsyncSP(TGrnheader objGRN, List<MItemMaster> objItems, int UserId, string Username);
        Task UpdateAsync(TGrnheader objGRN, List<MItemMaster> objItems, int UserId, string Username);
        Task InsertWithPOAsync(TGrnheader objGRN, List<MItemMaster> objItems, List<TPurchaseDetail> objPurDetails, List<TPurchaseHeader> objPurHeaders, int UserId, string Username);
        Task UpdateWithPOAsync(TGrnheader objGRN, List<MItemMaster> objItems, List<TPurchaseDetail> objPurDetails, List<TPurchaseHeader> objPurHeaders, int UserId, string Username);
        Task VerifyAsync(TGrndetail objGRN, int UserId, string Username);
    }
}
