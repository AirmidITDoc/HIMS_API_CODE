﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface IItemMasterService
    {
        Task InsertAsyncSP(MItemMaster objItemMaster, int UserId, string Username);
        Task InsertAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task UpdateAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task CancelAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task<IPagedList<ItemMasterListDto>> GetItemMasterListAsync(GridRequestModel objGrid);
        Task<MItemMaster> GetById(int Id);
        Task<List<ItemListForSearchDTO>> GetItemListForPrescription(int StoreId, string ItemName);
        Task<List<ItemListForSearchDTO>> GetItemListForGRNOrPO(int StoreId, string ItemName);
        Task<List<ItemListForBatchPopDTO>> GetItemListForSalesBatchPop(int StoreId, int ItemId);
        Task<List<ItemListForSalesPageDTO>> GetItemListForSalesPage(int StoreId, String ItemName);

    }
}
