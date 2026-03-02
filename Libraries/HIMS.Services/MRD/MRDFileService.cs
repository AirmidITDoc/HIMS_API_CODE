using HIMS.Data.Models;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.MRD;
using System.Data;
using HIMS.Services.Utilities;


namespace HIMS.Services.MRD
{
    public  class MRDFileService : IMRDFileService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MRDFileService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<MRDFileReceivedListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MRDFileReceivedListDto>(model, "ps_Rtrv_MRDFileReceivedList");
        }

        public virtual async Task InsertAsync(TMrdfileReceived ObjTMrdfileReceived, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TMrdfileReceiveds
                    .OrderByDescending(x => x.Mrdno)
                    .Select(x => x.Mrdno)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTMrdfileReceived.Mrdno = newSeqNo.ToString();

                _context.TMrdfileReceiveds.Add(ObjTMrdfileReceived);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TMrdfileReceived ObjTMrdfileReceived, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(  TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted  },  TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjTMrdfileReceived);
            _context.Entry(ObjTMrdfileReceived).State = EntityState.Modified;

            //  Ignore specific columns
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjTMrdfileReceived) .Property(column) .IsModified = false;
                }
            }
            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task InsertOutFileAsync(TMrdoutInFile ObjTMrdoutInFile, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "Opipid", "GivenUserId", "PersonName", "OutDate", "OutTime", "OutReason", "CreatedBy" };
            var entity = ObjTMrdoutInFile.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Insert_MRDOutInFile", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TMrdoutInFile), (int)ObjTMrdoutInFile.OutFileId, Core.Domain.Logging.LogAction.Add, UserId, Username);
        }
        public virtual async Task InsertInFileAsync(TMrdoutInFile ObjTMrdoutInFile, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "Opipid", "InDate", "InTime", "ReturnPersonName", "ReturnUserId", "InReason", "OutFileId", "ModifiedBy" };
            var entity = ObjTMrdoutInFile.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Update_MRDOutInFile_Return", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TMrdoutInFile), (int)ObjTMrdoutInFile.OutFileId, Core.Domain.Logging.LogAction.Edit, UserId, Username);
        }

    }
}
