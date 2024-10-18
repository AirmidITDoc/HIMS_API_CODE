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

                

        public virtual async Task UpdateAsyncSP(Bedmaster objBedMaster, int currentUserId, string currentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rBedEntity = { "IsAvailible", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var rAdmissentity1 = objBedMaster.ToDictionary();
            foreach (var rProperty in rBedEntity)
            {
                rAdmissentity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_update_BedMaster_1", CommandType.StoredProcedure, rAdmissentity1);
            // objAdmission.AdmissionId = Convert.ToInt32(objAdmission.AdmissionId);
        }
    }
}
