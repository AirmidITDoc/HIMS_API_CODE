using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IPrescriptionOPTemplateService
    {
        void InsertSP(MPresTemplateH ObjMPresTemplateH, List<MPresTemplateD> ObjMPresTemplateD, int UserId, string UserName);



    }
}
