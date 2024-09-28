using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public class BedTransferService : IBedTransferService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BedTransferService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        //public virtual async Task<TBedTransferDetail> InsertAsyncSP(TBedTransferDetail objBedTransferDetail, int UserId, string Username)
        //{


        //    DatabaseHelper odal = new();
        //    string[] rRefundEntity = { "TransferId" };
        //    var RefundEntity = objBedTransferDetail.ToDictionary();
        //    foreach (var rProperty in rRefundEntity)
        //    {
        //        RefundEntity.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("Insert_BedTransferDetails_1", CommandType.StoredProcedure, RefundEntity);
        //    await _context.SaveChangesAsync(UserId, Username);


        //}

        public virtual async Task InsertAsyncSP(TBedTransferDetail objBedTransferDetail, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
           

            string[] rEntity = {};
            var Entity = objBedTransferDetail.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_Insert_BedTransferDetails_1", CommandType.StoredProcedure, Entity);
            //await _context.SaveChangesAsync(UserId, Username);



        }
    }
}
