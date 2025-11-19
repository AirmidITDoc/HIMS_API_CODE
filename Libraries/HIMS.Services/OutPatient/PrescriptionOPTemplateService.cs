using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.OutPatient
{
    public class PrescriptionOPTemplateService : IPrescriptionOPTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PrescriptionOPTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual void InsertSP(MPresTemplateH ObjMPresTemplateH, List<MPresTemplateD> ObjMPresTemplateD, int UserId, string Username)
        {
            //Add header table records
            DatabaseHelper odal = new();
            var tokensObj = new
            {
                PresId = Convert.ToInt32(ObjMPresTemplateH.PresId)
            };
            odal.ExecuteNonQuery("Delete_M_PresTempl_1", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] rEntity = { "PresId", "PresTemplateName", "IsActive", "OpIpType", "TemplateCategory", "IsAddBy", "IsUpdatedBy", "CreatedBy" };
            var entity = ObjMPresTemplateH.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VPresId = odal.ExecuteNonQuery("insert_M_PresTemplateH_1", CommandType.StoredProcedure, "PresId", entity);
            ObjMPresTemplateH.PresId = Convert.ToInt32(VPresId);
            //ObjMPresTemplateD.PresId = Convert.ToInt32(VPresId);

            foreach (var item in ObjMPresTemplateD)
            {
                item.PresId = Convert.ToInt32(VPresId);

                string[] Entity = { "PresId", "Date", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "InstructionId", "QtyPerDay", "TotalQty", "Instruction", "Remark", "IsEnglishOrIsMarathi" };
                var Dentity = item.ToDictionary();
                foreach (var rProperty in Dentity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_M_PresTemplateD_1", CommandType.StoredProcedure, Dentity);

            }
        }

    }
}
