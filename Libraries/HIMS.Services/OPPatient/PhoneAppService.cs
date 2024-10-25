using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public class PhoneAppService : IPhoneAppService
    {
        private readonly HIMSDbContext _context;
        public PhoneAppService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
         public virtual async Task InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int UserId, string Username)
            {
            DatabaseHelper odal = new();
            string[] rEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate" };
            var entity = objTPhoneAppointment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }

            string PhoneAppId = odal.ExecuteNonQuery("v_insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
            objTPhoneAppointment.PhoneAppId = Convert.ToInt32(PhoneAppId);

            await _context.SaveChangesAsync(UserId, Username);


            //    public virtual async Task InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int UserId, string Username)
            //{
            //    DatabaseHelper odal = new();
            //    string[] rEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate" };
            //    var entity = objTPhoneAppointment.ToDictionary();

            //    foreach (var rProperty in rEntity)
            //    {
            //        entity.Remove(rProperty);
            //    }

            //    string phoneAppIdString = odal.ExecuteNonQuery("v_insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);

            //    if (int.TryParse(phoneAppIdString, out int phoneAppId))
            //    {
            //        objTPhoneAppointment.PhoneAppId = phoneAppId;
            //    }
            //    else
            //    {
            //        objTPhoneAppointment.PhoneAppId = 0;

            //        await _context.SaveChangesAsync(UserId, Username);
            //    }






        }
    }
}




