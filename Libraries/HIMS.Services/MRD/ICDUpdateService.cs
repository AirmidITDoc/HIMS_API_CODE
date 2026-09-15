using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace HIMS.Services.MRD
{
    public class ICDUpdateService : I_ICDUpdateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ICDUpdateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
     
        public virtual  async Task InsertICDSp(TPatIcdcdeH ObjTPatIcdcdeH, List<TPatIcdcdeD> ObjTPatIcdcdeDList,  int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
            string[] AEntity = {"Hid", "ReqDate", "ReqTime", "OpIpType", "OpIpId", "AddedBy", "UpdatedBy" };
            var entity = ObjTPatIcdcdeH.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                {
                    entity.Remove(rProperty);
                }
            }
            string vHid = odal.ExecuteNonQueryNew("insert_T_PatICDCdeH_1", CommandType.StoredProcedure, "Hid",entity);
            ObjTPatIcdcdeH.Hid = Convert.ToInt32(vHid);
            await _context.LogProcedureExecution(entity, nameof(TPatIcdcdeH), ObjTPatIcdcdeH.Hid.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


            foreach (var item in ObjTPatIcdcdeDList)
            {

                var tokensObj = new
                {
                    Hid = Convert.ToInt32(item.Hid)

                };
                odal.ExecuteNonQuery("Delete_T_PatICDCdeD", CommandType.StoredProcedure, tokensObj.ToDictionary());
            }

            foreach (var item in ObjTPatIcdcdeDList)
            {
                item.Hid = ObjTPatIcdcdeH.Hid;

                string[] PayEntity = { "Hid","IcdCode","IcdCodeDesc","AddedBy", "UpdatedBy", "MainIcdcdeId", "IcdcdeMainName"};

                var Dentity = item.ToDictionary();

                foreach (var rProperty in Dentity.Keys.ToList())
                {
                    if (!PayEntity.Contains(rProperty))
                    {
                        Dentity.Remove(rProperty);
                    }
                }

                odal.ExecuteNonQueryNew( "insert_T_PatICDCdeD_1", CommandType.StoredProcedure,"",Dentity);
                await _context.LogProcedureExecution(Dentity, nameof(TPatIcdcdeD), item.Did.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            }
            // Save Logs
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit Transaction
            await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
