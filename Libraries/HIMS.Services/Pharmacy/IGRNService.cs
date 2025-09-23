using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
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
        Task VerifyAsyncSp(TGrnheader objGRN, int UserId, string Username);
        Task AsyncSp(TGrnheader objGRN, int UserId, string Username);
        Task<IPagedList<ItemDetailsForGRNUpdateListDto>> GRNUpdateList(GridRequestModel objGrid);
        Task<IPagedList<GRNListDto>> GRNHeaderList(GridRequestModel objGrid);
        Task<IPagedList<GRNDetailsListDto>> GRNDetailsList(GridRequestModel objGrid);
        Task<IPagedList<DirectPOListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PoDetailListDto>> GetListAsync1(GridRequestModel objGrid);
        Task<IPagedList<InvoiceNoChecListDto>> InvoiceNoChecList(GridRequestModel objGrid);
        Task<List<BatchListDTO>> GetExisitingBatchList(int StoreId, int ItemId, string BatchNo);
        //Task<List<BatchListDTO>> GetGSTList(string GSTNo);
        Task<TGrnheader> GetById(int Id);
    
    }
}
