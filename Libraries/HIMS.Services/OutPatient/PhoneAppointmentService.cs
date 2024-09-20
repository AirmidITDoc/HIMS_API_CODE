using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Aspose.Cells.Drawing;


namespace HIMS.Services.OutPatient
{
    public class PhoneAppointmentService:IPhoneAppointmentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PhoneAppointmentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
      
        public virtual async Task InsertAsyncSP(TPhoneAppointment objPhappointment, int currentUserId, string currentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedByDate", "UpdatedByDate", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate"};
            var entity = objPhappointment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PhoneAppId = odal.ExecuteNonQuery("insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId ", entity);
            objPhappointment.PhoneAppId = Convert.ToInt32(PhoneAppId);
        }
    }
}
