using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public class AdminTaskService : IAdminTaskService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AdminTaskService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task BilldateUpdateAsync(Bill ObjBill, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "BillDate", "BillTime" };
            var Rentity = ObjBill.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_Administrtaion_BillDate", CommandType.StoredProcedure, Rentity);
            //await _context.LogProcedureExecution(Rentity, nameof(Bill), ObjBill.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }
        public virtual void Update(Admission ObjAdmission, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "AdmissionID", "AdmissionDate", "AdmissionTime"};
            var Rentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Update_AdmissionDateTime", CommandType.StoredProcedure, Rentity);
            //await _context.LogProcedureExecution(Rentity, nameof(Admission), ObjAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }

    }


}
