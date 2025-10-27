using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;

namespace HIMS.Services.MRD
{
    public partial interface I_MRDCertificate
    {
        Task<IPagedList<MRDCertificateListToDo>> MRDList(GridRequestModel objGrid);
    }
}
