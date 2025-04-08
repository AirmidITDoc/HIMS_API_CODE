using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial  interface IOPDPrescriptionMedicalService
    {
        Task InsertPrescriptionAsyncSP(List<TPrescription> objTPrescription,List<TOprequestList> objTOprequestList ,List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosisMaster, int UserId, string UserName);
        Task<IPagedList<GetVisitInfoListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionDetailsVisitWiseListDto>> GetListAsyncL(GridRequestModel objGrid);

        Task<IPagedList<MOpcasepaperDignosisMaster>> GetDignosisListAsync(GridRequestModel objGrid);
        Task<IPagedList<OPRequestListDto>> TOprequestList(GridRequestModel objGrid);
        Task<IPagedList<OPrtrvDignosisListDto>> TDignosisrRtrvList(GridRequestModel objGrid);
        Task<IPagedList<getPrescriptionTemplateDetailsListDto>> TemplateDetailsList(GridRequestModel objGrid);
        Task UpdateAsync(TPrescription ObjTPrescription, int UserId, string Username);
        Task UpdateAsyncGeneric(TPrescription ObjTPrescription, int UserId, string Username);
        //Task<List<OPrtrvDignosisListDto>> GetOPrtrvDignosisList(string ItemName);





    }
}
