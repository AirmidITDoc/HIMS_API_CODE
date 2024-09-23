using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public  class PhoneAppService: IPhoneAppService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PhoneAppService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objPhoneAppointment, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = {"SeqNo","IsCancelled","IsCancelledBy","IsCancelledDate","AddedByDate","UpdatedByDate","CreatedBy","CreatedDate", "ModifiedBy","ModifiedDate"};
            var entity = objPhoneAppointment.ToDictionary(); 
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PhoneAppId = odal.ExecuteNonQuery("insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
            objPhoneAppointment.PhoneAppId = Convert.ToInt32(PhoneAppId);


            await _context.SaveChangesAsync(UserId, Username);

            return objPhoneAppointment;
        }
    }
}
