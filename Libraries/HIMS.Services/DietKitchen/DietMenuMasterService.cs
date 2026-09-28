using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.GRN;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{

    public class DietMenuMasterService : IDietMenuMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietMenuMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<DietmenumasterListDto>> GetDietmenumasterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietmenumasterListDto>(model, "getMDietMenuMasterList");
        }
        public virtual async Task<IPagedList<DietmenuDetailmasterListDto>> GetDietmenumasterDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietmenuDetailmasterListDto>(model, "getMDietMenuDetailsMasterList");
        }
        public virtual async Task InsertAsync( MDietMenuMaster ObjHeader, List<MDietMenuDetailMaster> ObjDetailList, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();

                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());
                string[] HEntity = { "DietMenuId", "DietMenuName", "MealTypeId", "DietTypeId", "Texture", "Calories", "Protein", "CreatedBy" };

                var hentity = ObjHeader.ToDictionary();

                foreach (var rProperty in hentity.Keys.ToList())
                {
                    if (!HEntity.Contains(rProperty))
                        hentity.Remove(rProperty);
                }

                string VDietMenuId = odal.ExecuteNonQueryNew( "ps_Insert_DietMenuMaster", CommandType.StoredProcedure, "DietMenuId", hentity );
                ObjHeader.DietMenuId = Convert.ToInt64(VDietMenuId);
                await _context.LogProcedureExecution(  hentity,  nameof(MDietMenuMaster),  Convert.ToInt32(ObjHeader.DietMenuId),  Core.Domain.Logging.LogAction.Add,  CurrentUserId,CurrentUserName);

               
                foreach (var item in ObjDetailList)
                {
                    item.DietMenuId = ObjHeader.DietMenuId;

                    string[] DEntity ={"DietMenuId", "FoodItemId", "Quantity", "UnitId", "SequenceNo", "CreatedBy" };

                    var dEntity = item.ToDictionary();

                    foreach (var rProperty in dEntity.Keys.ToList())
                    {
                        if (!DEntity.Contains(rProperty))
                            dEntity.Remove(rProperty);
                    }

                    odal.ExecuteNonQueryNew( "ps_Insert_DietMenuDetailMaster", CommandType.StoredProcedure, "", dEntity );
                    await _context.LogProcedureExecution( dEntity, nameof(MDietMenuDetailMaster), Convert.ToInt32(item.MenuDetId), Core.Domain.Logging.LogAction.Add, CurrentUserId,  CurrentUserName);
                }

                await _context.SaveChangesAsync(  CurrentUserId,  CurrentUserName );
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        public virtual async Task UpdateAsync(MDietMenuMaster ObjHeader, List<MDietMenuDetailMaster> ObjDetailList, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                // Header update (DietMenuCode intentionally excluded)
                ObjHeader.ModifiedBy = CurrentUserId;

                string[] HEntity = { "DietMenuId", "DietMenuName", "MealTypeId", "DietTypeId", "Texture", "Calories", "Protein", "ModifiedBy" };
                var hentity = ObjHeader.ToDictionary();
                foreach (var rProperty in hentity.Keys.ToList())
                {
                    if (!HEntity.Contains(rProperty))
                        hentity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("ps_Update_DietMenuMaster", CommandType.StoredProcedure,"", hentity);

                await _context.LogProcedureExecution(hentity, nameof(MDietMenuMaster), Convert.ToInt32(ObjHeader.DietMenuId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                var tokensDelete = new
                {
                    DietMenuId = ObjHeader.DietMenuId
                };

                odal.ExecuteNonQueryNew("ps_Delete_M_DietMenuDetailMaster", CommandType.StoredProcedure,"", tokensDelete.ToDictionary());

                await _context.LogProcedureExecution(tokensDelete.ToDictionary(), nameof(MDietMenuDetailMaster), Convert.ToInt32(ObjHeader.DietMenuId), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                if (ObjDetailList != null && ObjDetailList.Any())
                {
                    foreach (var item in ObjDetailList)
                    {
                        item.DietMenuId = ObjHeader.DietMenuId;
                        item.CreatedBy = CurrentUserId;

                        string[] DEntity = { "DietMenuId", "FoodItemId", "Quantity", "UnitId", "SequenceNo", "CreatedBy" };
                        var dEntity = item.ToDictionary();
                        foreach (var rProperty in dEntity.Keys.ToList())
                        {
                            if (!DEntity.Contains(rProperty))
                                dEntity.Remove(rProperty);
                        }

                        odal.ExecuteNonQueryNew("ps_Insert_DietMenuDetailMaster", CommandType.StoredProcedure,"", dEntity);

                        await _context.LogProcedureExecution(dEntity, nameof(MDietMenuDetailMaster), Convert.ToInt32(item.DietMenuId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                    }
                }

                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    }
   