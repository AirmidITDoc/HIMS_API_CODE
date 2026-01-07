using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class ItemMasterServices : IItemMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ItemMasterServices(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ItemMasterListDto>> GetItemMasterListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemMasterListDto>(model, "m_Rtrv_ItemMaster_by_Name_Pagi");
        }


        public virtual async Task<MItemMaster> GetById(int Id)
        {
            return await this._context.MItemMasters.Include(x => x.MAssignItemToStores).FirstOrDefaultAsync(x => x.ItemId == Id);
        }

        public virtual async Task InsertAsyncSP(MItemMaster objItemMaster, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { " UpDatedBy", "IsNarcotic", "IsUpdatedBy", "CreatedBy", "CreatedDate", " ItemTime", "MAssignItemToStore" };
                var entity = objItemMaster.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vItemId = odal.ExecuteNonQuery("Insert_ItemMaster_1_New", CommandType.StoredProcedure, "ItemId", entity);
                objItemMaster.ItemId = Convert.ToInt32(vItemId);

                // Add details table records
                foreach (var objAssign in objItemMaster.MAssignItemToStores)
                {
                    objAssign.ItemId = objItemMaster.ItemId;
                }
                _context.MAssignItemToStores.AddRange(objItemMaster.MAssignItemToStores);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                MItemMaster? objSup = await _context.MItemMasters.FindAsync(objItemMaster.ItemId);
                if (objSup != null)
                {
                    _context.MItemMasters.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.MAssignItemToStores.Where(x => x.ItemId == objItemMaster.ItemId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MAssignItemToStores.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task InsertAsync(MItemMaster objItemMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MItemMasters.Add(objItemMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(MItemMaster objItemMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // 1. Attach the entity without marking everything as modified
                _context.Attach(objItemMaster);
                _context.Entry(objItemMaster).State = EntityState.Modified;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(objItemMaster).Property(column).IsModified = false;
                    }
                }

                // Delete details table realted records
                var lst = await _context.MAssignItemToStores.Where(x => x.ItemId == objItemMaster.ItemId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MAssignItemToStores.RemoveRange(lst);
                }
                //await _context.SaveChangesAsync();

                // Update header & detail table records
                //_context.MItemMasters.Update(objItemMaster);
                //_context.Entry(objItemMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }

        }
        //public virtual async Task UpdateAsync(MItemMaster objItemMaster, int UserId, string Username)
        //{
        //     using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //      // Delete related store assignments
        //    var lst = await _context.MAssignItemToStores .Where(x => x.ItemId == objItemMaster.ItemId) .ToListAsync();
        //     if (lst.Count > 0)
        //        _context.MAssignItemToStores.RemoveRange(lst);

        //        await _context.SaveChangesAsync();

        //    var existing = await _context.MItemMasters .FirstOrDefaultAsync(x => x.ItemId == objItemMaster.ItemId);

        //    // Manually copy only the fields you want to update (do NOT touch CreatedBy/CreatedDate)
        //    existing.ItemShortName = objItemMaster.ItemShortName;
        //    existing.ItemName = objItemMaster.ItemName;
        //    existing.ItemTypeId = objItemMaster.ItemTypeId;
        //    existing.ItemCategaryId = objItemMaster.ItemCategaryId;
        //    existing.ItemGenericNameId = objItemMaster.ItemGenericNameId;
        //    existing.ItemClassId = objItemMaster.ItemClassId;
        //    existing.PurchaseUomid = objItemMaster.PurchaseUomid;
        //    existing.ItemClassId = objItemMaster.ItemClassId;
        //    existing.StockUomid = objItemMaster.StockUomid;
        //    existing.ConversionFactor = objItemMaster.ConversionFactor;
        //    existing.CurrencyId = objItemMaster.CurrencyId;
        //    existing.TaxPer = objItemMaster.TaxPer;
        //    existing.IsActive = objItemMaster.IsActive;
        //    existing.IsBatchRequired = objItemMaster.IsBatchRequired;
        //    existing.MinQty = objItemMaster.MinQty;
        //    existing.MaxQty = objItemMaster.MaxQty;
        //    existing.ReOrder = objItemMaster.ReOrder;
        //    existing.Hsncode = objItemMaster.Hsncode;
        //    existing.Cgst = objItemMaster.Cgst;
        //    existing.Sgst = objItemMaster.Sgst;
        //    existing.Igst = objItemMaster.Igst;
        //    existing.ManufId = objItemMaster.ManufId;
        //    existing.IsNarcotic = objItemMaster.IsNarcotic;
        //    existing.IsH1drug = objItemMaster.IsH1drug;
        //    existing.IsScheduleH = objItemMaster.IsScheduleH;
        //    existing.IsHighRisk = objItemMaster.IsHighRisk;
        //    existing.IsScheduleX = objItemMaster.IsScheduleX;
        //    existing.IsLasa = objItemMaster.IsLasa;
        //    existing.DrugType = objItemMaster.DrugType;
        //    existing.DrugTypeName = objItemMaster.DrugTypeName;
        //    existing.ProdLocation = objItemMaster.ProdLocation;
        //    existing.ItemCompnayId = objItemMaster.ItemCompnayId;
        //    existing.ItemTime = objItemMaster.ItemTime;
        //    existing.Addedby = objItemMaster.Addedby;
        //    existing.UpDatedBy = objItemMaster.UpDatedBy;
        //    existing.DoseDay = objItemMaster.DoseDay;
        //    existing.Instruction = objItemMaster.Instruction;

        //    // Update audit fields
        //    existing.ModifiedBy = UserId;
        //    existing.ModifiedDate = AppTime.Now;
        //    existing.IsUpdatedBy = AppTime.Now;

        //    await _context.SaveChangesAsync();

        //    scope.Complete();
        //}



        public virtual async Task CancelAsync(MItemMaster objItemMaster, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MItemMaster objItem = await _context.MItemMasters.FindAsync(objItemMaster.ItemId);
                objItem.IsActive = false;
                objItem.CreatedDate = objItemMaster.CreatedDate;
                objItem.CreatedBy = objItemMaster.CreatedBy;
                _context.MItemMasters.Update(objItem);
                _context.Entry(objItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task<List<ItemListForSearchDTO>> GetItemListForPrescription(int StoreId, string ItemName)
        {
            var qry = (from itemMaster in _context.MItemMasters
                       join uomMaster in _context.MUnitofMeasurementMasters
                       on itemMaster.PurchaseUomid equals uomMaster.UnitofMeasurementId
                       join genericNameMaster in _context.MItemGenericNameMasters
                       on itemMaster.ItemGenericNameId equals genericNameMaster.ItemGenericNameId
                       join assignItemToStore in _context.MAssignItemToStores
                       on itemMaster.ItemId equals assignItemToStore.ItemId into storeGroup
                       from assignItem in storeGroup.DefaultIfEmpty()
                       where (string.IsNullOrEmpty(ItemName) || itemMaster.ItemName.Contains(ItemName))
                          && (assignItem == null || assignItem.StoreId == StoreId)
                       orderby itemMaster.ItemId

                       select new ItemListForSearchDTO
                       {
                           //StoreId = assignItem != null ? assignItem.StoreId : 0,
                           ItemId = itemMaster.ItemId,
                           ItemName = itemMaster.ItemName,
                           BalanceQty = 0,
                           LandedRate = 0,
                           UnitMRP = 0,
                           PurchaseRate = 0,
                           //VatPercentage = 0,
                           //itemMaster.IsBatchRequired,
                           //ReOrder = itemMaster.ReOrder,
                           //IsNarcotic = itemMaster.IsNarcotic ?? 0,
                           //CGSTPer = itemMaster.CGST,
                           //SGSTPer = itemMaster.SGST,
                           //IGSTPer = itemMaster.IGST,
                           //UOM = uomMaster.UnitofMeasurementName,
                           //itemMaster.ItemGenericNameId,
                           //itemGenericNameMaster.ItemGenericName,
                           DoseName = itemMaster.DoseName ?? string.Empty,
                           DoseDay = itemMaster.DoseDay ?? 0,
                           Instruction = itemMaster.Instruction ?? string.Empty
                       });
            return await qry.Take(50).ToListAsync();
        }
        public virtual List<ItemListForSearchDTO> GetItemListForPrescriptionretrun(int StoreId, int IPAdmID, string ItemName)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@StoreId", StoreId);
            para[1] = new SqlParameter("@IPAdmID", IPAdmID);
            para[2] = new SqlParameter("@ItemName", ItemName);
            
            return sql.FetchListBySP<ItemListForSearchDTO>("ps_Rtrv_IPSalesReturnDrugForNursing", para);
        }
        public virtual async Task<List<ItemListForSearchDTO>> GetItemListForGRNOrPO(int StoreId, string ItemName)
        {
            var qry = (from itemMaster in _context.MItemMasters
                       join uomMaster in _context.MUnitofMeasurementMasters
                       on itemMaster.PurchaseUomid equals uomMaster.UnitofMeasurementId
                       join assignItemToStore in _context.MAssignItemToStores
                       on itemMaster.ItemId equals assignItemToStore.ItemId
                       join itemCompanyMaster in _context.MItemCompanyMasters
                       on itemMaster.ItemCompnayId equals itemCompanyMaster.CompanyId into companyGroup
                       from company in companyGroup.DefaultIfEmpty()
                       join currentStock in _context.TCurrentStocks
                       on itemMaster.ItemId equals currentStock.ItemId into stockGroup
                       from stock in stockGroup.DefaultIfEmpty()
                       where (string.IsNullOrEmpty(ItemName) || itemMaster.ItemName.Contains(ItemName))
                          && assignItemToStore.StoreId == StoreId
                       group new { itemMaster, uomMaster, assignItemToStore, stock, company } by new
                       {
                           itemMaster.ItemId,
                           itemMaster.ItemName,
                           uomMaster.UnitofMeasurementName,
                           uomMaster.UnitofMeasurementId,
                           itemMaster.ConversionFactor,
                           itemMaster.TaxPer,
                           itemMaster.IsBatchRequired,
                           assignItemToStore.StoreId,
                           itemMaster.ItemCategaryId,
                           itemMaster.Cgst,
                           itemMaster.Sgst,
                           itemMaster.Igst,
                           itemMaster.Hsncode,
                           company.CompanyName
                       } into grouped
                       select new ItemListForSearchDTO
                       {
                           ItemId = grouped.Key.ItemId,
                           ItemName = grouped.Key.ItemName,
                           //grouped.Key.UnitofMeasurementName,
                           UMOId = grouped.Key.UnitofMeasurementId,
                           UMOName = grouped.Key.UnitofMeasurementName,
                           ConverFactor = grouped.Key.ConversionFactor,
                           //grouped.Key.TaxPer,
                           //grouped.Key.IsBatchRequired,
                           StoreId = grouped.Key.StoreId,
                           //grouped.Key.ItemCategaryId,
                           BalanceQty = grouped.Sum(x => x.stock != null ? x.stock.BalanceQty : 0),
                           CGSTPer = grouped.Key.Cgst ?? 0,
                           SGSTPer = grouped.Key.Sgst ?? 0,
                           IGSTPer = grouped.Key.Igst ?? 0,
                           HSNcode = grouped.Key.Hsncode,
                           ItemCompanyName = grouped.Key.CompanyName ?? string.Empty,
                           TaxPer = grouped.Key.TaxPer
                       });
            return await qry.Take(50).ToListAsync();
        }

        public List<ItemListForBatchDTO> ItemListForBatch(int StoreId, int ItemId, int PatientTypeId)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@StoreId", StoreId);
            para[1] = new SqlParameter("@ItemId", ItemId);       
            para[2] = new SqlParameter("@PatientTypeId", PatientTypeId); 

            List<ItemListForBatchDTO> lstServiceList = sql.FetchListBySP<ItemListForBatchDTO>("ps_Rtrv_ItemName_BatchPOP_BalanceQty", para);
            return lstServiceList;
        }

        public virtual async Task<List<ItemListForBatchPopDTO>> GetItemListForSalesBatchPop(int StoreId, int ItemId)
        {

            //var qry = (from currentStock in _context.TCurrentStocks
            //             join itemMaster in _context.MItemMasters
            //             on currentStock.ItemId equals itemMaster.ItemId
            //             join itemManufactureMaster in _context.MItemManufactureMasters
            //             on itemMaster.ManufId equals itemManufactureMaster.ItemManufactureId into manufactureGroup
            //             from manufacture in manufactureGroup.DefaultIfEmpty()
            //             where currentStock.ItemId == ItemId
            //                && currentStock.StoreId == StoreId
            //                && (currentStock.BalanceQty - (currentStock.GrnRetQty ?? 0)) > 0
            //             orderby currentStock.BalanceQty descending
            //             select new ItemListForBatchPopDTO
            //             {
            //                 StockId=currentStock.StockId,
            //                 StoreId= currentStock.StoreId,
            //                 ItemId = currentStock.ItemId,
            //                 ItemName = itemMaster.ItemName,
            //                 BalanceQty = currentStock.BalanceQty - (currentStock.GrnRetQty ?? 0),
            //                 LandedRate = currentStock.LandedRate,
            //                 UnitMRP = currentStock.UnitMrp,
            //                 PurchaseRate = currentStock.PurUnitRateWf,
            //                 VatPercentage = currentStock.VatPercentage,
            //                 //itemMaster.IsBatchRequired,
            //                 BatchNo = currentStock.BatchNo,
            //                 BatchExpDate = currentStock.BatchExpDate,
            //                 ConverFactor = itemMaster.ConversionFactor,
            //                 CGSTPer = currentStock.Cgstper,
            //                 SGSTPer = currentStock.Sgstper,
            //                 IGSTPer = currentStock.Igstper,
            //                 ManufactureName = manufacture != null ? manufacture.ManufactureName : string.Empty,
            //                 GrnRetQty = currentStock.GrnRetQty,
            //                 DrugTypeName = itemMaster.DrugTypeName
            //             });
            //return await qry.Take(50).ToListAsync();

            var qry =
                (from cs in _context.TCurrentStocks
                 join im in _context.MItemMasters
                     on cs.ItemId equals im.ItemId
                 join gm in _context.MItemGenericNameMasters
          on im.ItemGenericNameId equals gm.ItemGenericNameId
                 join mf in _context.MItemManufactureMasters
                     on im.ManufId equals mf.ItemManufactureId into manufactureGroup
                 from mf in manufactureGroup.DefaultIfEmpty() // LEFT JOIN
                 where cs.ItemId == ItemId
        && cs.StoreId == StoreId
        && (cs.BalanceQty - (cs.GrnRetQty ?? 0)) > 0
                 orderby cs.BalanceQty descending
                 select new ItemListForBatchPopDTO
                 {
                     StockId = cs.StockId,
                     StoreId = cs.StoreId,
                     ItemId = cs.ItemId,
                     ItemName = im.ItemName,
                     MinQty = im.MinQty,
                     MaxQty = im.MaxQty,
                     ItemGenericNameId = gm.ItemGenericNameId,
                     ItemGenericName = gm.ItemGenericName,
                     ProdLocation = im.ProdLocation,
                     BalanceQty = cs.BalanceQty - (cs.GrnRetQty ?? 0),
                     LandedRate = cs.LandedRate,
                     UnitMRP = cs.UnitMrp,
                     PurchaseRate = cs.PurUnitRateWf,
                     VatPercentage = cs.VatPercentage,
                     //IsBatchRequired = im.IsBatchRequired,
                     BatchNo = cs.BatchNo,
                     BatchExpDate = cs.BatchExpDate,
                     ConversionFactor = im.ConversionFactor,
                     CGSTPer  = cs.Cgstper,
                     SGSTPer = cs.Sgstper ,
                     IGSTPer  = cs.Igstper,
                     //IsNarcotic = im.IsNarcotic ?? 0,
                     ManufactureName = mf != null ? mf.ManufactureName : string.Empty,
                     //IsHighRisk = cs.IsHighRisk,
                     //IsEmgerency = cs.IsEmgerency,
                     //IsLASA = cs.IsLasa,
                     //IsH1Drug = cs.IsH1Drug,
                     GrnRetQty = cs.GrnRetQty,
                     //ExpDays = EF.Functions.DateDiffDay(cs.BatchExpDate, AppTime.Now),
                     ExpDays = EF.Functions.DateDiffDay(AppTime.Now, cs.BatchExpDate),
                     DaysFlag = (EF.Functions.DateDiffDay(AppTime.Now, cs.BatchExpDate) < 30) ? 1
                               : (EF.Functions.DateDiffDay(AppTime.Now, cs.BatchExpDate) > 50) ? 2
                               : 3,
                     DrugTypeName = im.DrugTypeName
                 });

            return await qry.Take(50).ToListAsync();

        }
        public virtual async Task<List<ItemListForSalesPageDTO>> GetItemListForSalesPage(int StoreId, String ItemName)
        {
            var qry = (
                from item in _context.MItemMasters
                join uom in _context.MUnitofMeasurementMasters
                    on item.PurchaseUomid equals uom.UnitofMeasurementId
                join stock in _context.TCurrentStocks
                    on item.ItemId equals stock.ItemId into stockGroup
                from stock in stockGroup.DefaultIfEmpty()
                join gen in _context.MItemGenericNameMasters
                    on item.ItemGenericNameId equals gen.ItemGenericNameId into genGroup
                from gen in genGroup.DefaultIfEmpty()
                where (string.IsNullOrEmpty(ItemName) || item.ItemName.Contains(ItemName))
                      && stock.StoreId == StoreId
                group new { item, stock, uom, gen } by new
                {
                    stock.StoreId,
                    item.ItemId,
                    item.ItemName,
                    item.IsBatchRequired,
                    item.IsNarcotic,
                    uom.UnitofMeasurementName,
                    item.ItemGenericNameId,
                    gen.ItemGenericName,
                    item.IsHighRisk,
                    item.IsEmgerency,
                    item.IsLasa,
                    item.IsH1drug,
                    item.DoseName,
                    item.DoseDay,
                    item.Instruction
                } into g
                select new ItemListForSalesPageDTO
                {
                    StoreId = g.Key.StoreId,
                    ItemId = g.Key.ItemId,
                    ItemName = g.Key.ItemName + " | Bal Qty : " + g.Sum(x => (x.stock.BalanceQty ?? 0) - (x.stock.GrnRetQty ?? 0)) + " | Price : " + g.Max(x => x.stock.UnitMrp),
                    BalanceQty = g.Sum(x => (x.stock.BalanceQty ?? 0) - (x.stock.GrnRetQty ?? 0)),
                    LandedRate = g.Max(x => x.stock.LandedRate),
                    UnitMRP = g.Max(x => x.stock.UnitMrp),
                    PurchaseRate = g.Max(x => x.stock.PurUnitRateWf),
                    //VatPercentage = g.Max(x => x.stock.VatPercentage),
                    //g.Key.IsBatchRequired,
                    //ReOrder = g.Max(x => x.item.ReOrder),
                    //IsNarcotic = g.Key.IsNarcotic ?? false,
                    CGSTPer = g.Max(x => x.stock.Cgstper),
                    SGSTPer = g.Max(x => x.stock.Sgstper),
                    IGSTPer = g.Max(x => x.stock.Igstper),
                    UOM = g.Key.UnitofMeasurementName,
                    //g.Key.ItemGenericNameId,
                    //g.Key.ItemGenericName,
                    //g.Key.IsHighRisk,
                    //g.Key.IsEmgerency,
                    //g.Key.IsLasa,
                    //g.Key.IsH1drug,
                    //DoseName = g.Key.DoseName ?? "",
                    //DoseDay = g.Key.DoseDay ?? 0,
                    //Instruction = g.Key.Instruction ?? ""
                }
            );
            return await qry.Take(50).ToListAsync();
        }

        public List<ItemListForBatchPopDTO> SearchGetItemListForSalesBatchPop(int StoreId, int ItemId, int PatientTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
