using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public class BedTransferService : IBedTransferService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BedTransferService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task InsertAsyncSP(TBedTransferDetail objBedTransferDetail, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();


            string[] rEntity = { "TransferId" };
            var Entity = objBedTransferDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_Insert_BedTransferDetails_1", CommandType.StoredProcedure, Entity);
            //await _context.SaveChangesAsync(UserId, Username);
        }





        //public virtual async Task InsertAsyncSP(TBedTransferDetail objBedTransferDetail,  int CurrentUserId, string CurrentUserName)
        //{
        //    //// NEW CODE With EDMX
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Add Registration table records
        //        _context.TBedTransferDetails.Add(objBedTransferDetail);
        //        await _context.SaveChangesAsync();

        //        // Update Registration table records
        //        ConfigSetting objConfigRSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
        //        objConfigRSetting.RegNo = Convert.ToString(Convert.ToInt32(objConfigRSetting.RegNo) + 1);
        //        _context.ConfigSettings.Update(objConfigRSetting);
        //        _context.Entry(objConfigRSetting).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();


        //    }
        //}
    }
}
