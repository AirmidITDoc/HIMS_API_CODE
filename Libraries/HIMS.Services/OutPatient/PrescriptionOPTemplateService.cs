using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public  class PrescriptionOPTemplateService : IPrescriptionOPTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PrescriptionOPTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(MPresTemplateH ObjMPresTemplateH, List<MPresTemplateD> ObjMPresTemplateD, int UserId, string Username)
        {
            //Add header table records
            DatabaseHelper odal = new();
            var tokensObj = new
            {
                PresId = Convert.ToInt32(ObjMPresTemplateH.PresId)
            };
            odal.ExecuteNonQuery("Delete_M_PresTempl_1", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] rEntity = { "IsUpdatedBy", "CreatedBy", "ModifiedBy", "ModifiedDate", "CreatedDate", };
            var entity = ObjMPresTemplateH.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VPresId = odal.ExecuteNonQuery("insert_M_PresTemplateH_1", CommandType.StoredProcedure, "PresId", entity);
            ObjMPresTemplateH.PresId = Convert.ToInt32(VPresId);
            //ObjMPresTemplateD.PresId = Convert.ToInt32(VPresId);

            foreach (var item in ObjMPresTemplateD)
            {
                item.PresId = Convert.ToInt32(VPresId);


                string[] Entity = { "PresDetId" };
                var Dentity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_M_PresTemplateD_1", CommandType.StoredProcedure, Dentity);

            }
        }

    }
}
