using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
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
    public class PhoneAppointment1Service : IPhoneAppointment1Service
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PhoneAppointment1Service(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedByDate", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var entity = objTPhoneAppointment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PhoneAppId = odal.ExecuteNonQuery("v_insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
            objTPhoneAppointment.PhoneAppId = Convert.ToInt32(PhoneAppId);


            await _context.SaveChangesAsync(UserId, Username);

            return objTPhoneAppointment;
        }

       
    }
}
