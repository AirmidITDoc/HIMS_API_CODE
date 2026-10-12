using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OTManagment
{
    public class BillTemplateService : IBillTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BillTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<MOtBillTemplateHeader> GetById(long Id)
        {
            return await this._context.MOtBillTemplateHeaders.Include(x => x.MOtBillTemplateDetails).FirstOrDefaultAsync(x => x.OtbtempId == Id);
        }
        public virtual async Task<IPagedList<OtBillTemplateListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OtBillTemplateListDto>(model, "ps_ot_billTempleHeaderlist");
        }
        public virtual async Task InsertAsync(MOtBillTemplateHeader ObjMOtBillTemplateHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                //var lastSeqNoStr = await _context.MOtBillTemplateHeaders
                //    .OrderByDescending(x => x.OtreservationNo)
                //    .Select(x => x.OtreservationNo)
                //    .FirstOrDefaultAsync();

                //int lastSeqNo = 0;
                //if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                //    lastSeqNo = parsed;

                //// Increment the sequence number
                //int newSeqNo = lastSeqNo + 1;
                //ObjTOtReservationHeader.OtreservationNo = newSeqNo.ToString();


                ObjMOtBillTemplateHeader.CreatedBy = UserId;
                ObjMOtBillTemplateHeader.CreatedDateTime = AppTime.Now;

                _context.MOtBillTemplateHeaders.Add(ObjMOtBillTemplateHeader);
                await _context.SaveChangesAsync(UserId, Username);       // fixed


                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MOtBillTemplateHeader ObjMOtBillTemplateHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long OtbtempId = ObjMOtBillTemplateHeader.OtbtempId;

                // Delete related details first
                var lstAttend = await _context.MOtBillTemplateDetails
                    .Where(x => x.OtbtempId == OtbtempId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.MOtBillTemplateDetails.RemoveRange(lstAttend);


                //Save deletion first
                await _context.SaveChangesAsync(UserId, Username);       // fixed

                // Then attach and update header
                _context.Attach(ObjMOtBillTemplateHeader);
                _context.Entry(ObjMOtBillTemplateHeader).State = EntityState.Modified;

                _context.Entry(ObjMOtBillTemplateHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjMOtBillTemplateHeader).Property(x => x.CreatedDateTime).IsModified = false;

                ObjMOtBillTemplateHeader.ModifiedBy = UserId;
                ObjMOtBillTemplateHeader.ModifiedDateTime = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjMOtBillTemplateHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync(UserId, Username);       // fixed

                scope.Complete();
            }
        }
    }
}
