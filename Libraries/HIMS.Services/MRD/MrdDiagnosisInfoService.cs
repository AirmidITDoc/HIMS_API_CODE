using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.MRD
{
    public  class MRDDiagnosisInfoService : IMrdDiagnosisInfoService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MRDDiagnosisInfoService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<MrdDiagnosisInfoListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MrdDiagnosisInfoListDto>(model, "ps_Rtrv_IP_MRD_DiagnosisInfo");
        }

        //public virtual async Task InsertAsync(TIpMrdDiagnosisInfoHeader ObjTIpMrdDiagnosisInfoHeader,int UserId,string Username)
        //{
        //    using var scope = new TransactionScope(
        //        TransactionScopeOption.Required,
        //        new TransactionOptions
        //        {
        //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //        },
        //        TransactionScopeAsyncFlowOption.Enabled);

        //    _context.TIpMrdDiagnosisInfoHeaders.Add(ObjTIpMrdDiagnosisInfoHeader);

        //    await _context.SaveChangesAsync();

        //    scope.Complete();
        //}
        public virtual async Task InsertAsync(TIpMrdDiagnosisInfoHeader ObjHeader,List<TIpMrdDiagnosisInfoDetail> ObjDetailList,int CurrentUserId, string CurrentUserName)

        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                string[] HEntity = { "IpdiagId", "AdmId", "IsSync", "CreatedBy" };

                var hentity = ObjHeader.ToDictionary();
                foreach (var rProperty in hentity.Keys.ToList())
                {
                    if (!HEntity.Contains(rProperty))
                        hentity.Remove(rProperty);
                }

                string VIpdiagId = odal.ExecuteNonQueryNew(  "ps_TIpMrdDiagnosisInfoHeader_Insert",  CommandType.StoredProcedure,  "IpdiagId",  hentity );

                ObjHeader.IpdiagId = Convert.ToInt64(VIpdiagId);

                await _context.LogProcedureExecution( hentity, nameof(TIpMrdDiagnosisInfoHeader), Convert.ToInt32(ObjHeader.IpdiagId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName );

             
                foreach (var item in ObjDetailList)
                {
                    item.IpdiagId = Convert.ToInt32(VIpdiagId);
                    string[] DEntity = { "IpdiagId", "AdmId", "Diagnosis", "Icdcode", "Diagnosisinformation", "FlagCode", "CreatedBy" };
                    var dEntity = item.ToDictionary();
                    foreach (var rProperty in dEntity.Keys.ToList())
                    {
                        if (!DEntity.Contains(rProperty))
                            dEntity.Remove(rProperty);
                    }
                    odal.ExecuteNonQueryNew("ps_TIpMrdDiagnosisInfoDetail_Insert", CommandType.StoredProcedure,"", dEntity);
                    await _context.LogProcedureExecution(dEntity, nameof(TIpMrdDiagnosisInfoDetail), Convert.ToInt32(item.IpdiagId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                }

                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        //public virtual async Task UpdateAsync( TIpMrdDiagnosisInfoHeader ObjTIpMrdDiagnosisInfoHeader, int UserId, string Username, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope( TransactionScopeOption.Required, new TransactionOptions  { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    // Attach existing Header
        //    _context.Attach(ObjTIpMrdDiagnosisInfoHeader);

        //    _context.Entry(ObjTIpMrdDiagnosisInfoHeader).State = EntityState.Modified;

        //    // Don't update Created fields
        //    _context.Entry(ObjTIpMrdDiagnosisInfoHeader).Property(x => x.CreatedBy).IsModified = false;

        //    _context.Entry(ObjTIpMrdDiagnosisInfoHeader).Property(x => x.CreatedDate).IsModified = false;


        //    ObjTIpMrdDiagnosisInfoHeader.ModifiedBy = UserId;
        //    ObjTIpMrdDiagnosisInfoHeader.ModifiedDate = AppTime.Now;

        //    // Ignore columns
        //    if (ignoreColumns?.Length > 0)
        //    {
        //        foreach (var column in ignoreColumns)
        //        {
        //            _context.Entry(ObjTIpMrdDiagnosisInfoHeader).Property(column).IsModified = false;
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    scope.Complete();
        //}
        public virtual async Task UpdateAsync(TIpMrdDiagnosisInfoHeader ObjHeader, List<TIpMrdDiagnosisInfoDetail> ObjDetailList, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection());
                odal.SetTransaction(transaction.GetDbTransaction());

                string[] HEntity = { "IpdiagId", "AdmId", "IsSync", "ModifiedBy" };
                var hentity = ObjHeader.ToDictionary();
                foreach (var rProperty in hentity.Keys.ToList())
                {
                    if (!HEntity.Contains(rProperty))
                        hentity.Remove(rProperty);
                }

                odal.ExecuteNonQuery("ps_TIpMrdDiagnosisInfoHeader_Update", CommandType.StoredProcedure, hentity);

                await _context.LogProcedureExecution(hentity, nameof(TIpMrdDiagnosisInfoHeader), Convert.ToInt32(ObjHeader.IpdiagId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                // Delete ALL existing Details related 3rd  Table
                var tokensDelete = new
                {
                    IpdiagId = ObjHeader.IpdiagId,
                    AdmId = ObjHeader.AdmId
                };

                odal.ExecuteNonQuery("ps_TIpMrdDiagnosisInfoDetail_Delete", CommandType.StoredProcedure, tokensDelete.ToDictionary());

                await _context.LogProcedureExecution(tokensDelete.ToDictionary(), nameof(TIpMrdDiagnosisInfoDetail), Convert.ToInt32(ObjHeader.IpdiagId), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

                if (ObjDetailList != null && ObjDetailList.Any())
                {
                    foreach (var item in ObjDetailList)
                    {
                        item.IpdiagId = ObjHeader.IpdiagId;
                        item.CreatedBy = CurrentUserId;

                        string[] DEntity = { "IpdiagId", "AdmId", "Diagnosis", "Icdcode", "Diagnosisinformation", "FlagCode", "CreatedBy" };
                        var dEntity = item.ToDictionary();
                        foreach (var rProperty in dEntity.Keys.ToList())
                        {
                            if (!DEntity.Contains(rProperty))
                                dEntity.Remove(rProperty);
                        }

                        odal.ExecuteNonQuery("ps_TIpMrdDiagnosisInfoDetail_Insert", CommandType.StoredProcedure, dEntity);

                        await _context.LogProcedureExecution(dEntity, nameof(TIpMrdDiagnosisInfoDetail), Convert.ToInt32(item.IpdiagId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                    }
                }

                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

