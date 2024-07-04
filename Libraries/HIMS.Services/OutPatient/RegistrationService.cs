using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class RegistrationService :IRegistrationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public RegistrationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(Registration objRegistration, int UserId, string Username)
        {
            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "RegNo", "UpdatedBy", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember" };
                var entity = objRegistration.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                entity.Remove("RegID");
                string RegId = odal.ExecuteNonQuery("m_insert_Registration_1", CommandType.StoredProcedure, "RegID", entity);
                objRegistration.RegId = Convert.ToInt32(RegId);

                await _context.SaveChangesAsync(UserId, Username);
            }
            catch (Exception ex)
            {
                // Delete header table realted records
                Registration objReg = await _context.Registrations.FindAsync(objRegistration.RegId);
                if (objReg != null)
                {
                    _context.Registrations.Remove(objReg);
                }
                await _context.SaveChangesAsync();
            }
        }

    }
}
