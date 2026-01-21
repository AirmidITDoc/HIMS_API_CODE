using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IItemMasterService
    {
        Task InsertAsyncSP(MItemMaster objItemMaster, int UserId, string Username);
        Task InsertAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task UpdateAsync(MItemMaster objItemMaster, int UserId, string Username, string[]? references);
        Task CancelAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task<IPagedList<ItemMasterListDto>> GetItemMasterListAsync(GridRequestModel objGrid);

        Task<IPagedList<ItemListForGRNOrPO>> GetItemMasterBySpListAsync(GridRequestModel objGrid);
        Task<MItemMaster> GetById(int Id);
       Task<List<ItemListForSearchDTO>> GetItemListForPrescription(int StoreId, string ItemName);
       List<ItemListForSearch> GetItemListForPrescriptionSearch( string ItemName, int StoreId);

        Task<List<ItemListForSearchDTO>> GetItemListForGRNOrPO(int StoreId, string ItemName);
        Task<List<ItemListForBatchPopDTO>> GetItemListForSalesBatchPop(int StoreId, int ItemId);
        Task<List<ItemListForSalesPageDTO>> GetItemListForSalesPage(int StoreId, String ItemName);
        List<ItemListForSearchDTO> GetItemListForPrescriptionretrun(int StoreId,  int IPAdmID, String ItemName);
        //List<ItemListForBatchPopDTO> SearchGetItemListForSalesBatchPop(int StoreId, int ItemId, int PatientTypeId);
        List<ItemListForBatchDTO> ItemListForBatch(int StoreId, int ItemId, int PatientTypeId);

        List<ItemListForGRNOrPO> ItemListForIndent(int StoreId, string ItemName);


    }
}
