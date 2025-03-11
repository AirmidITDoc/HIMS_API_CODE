using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.OutPatient.TPrescriptionModel;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPDPrescriptionMedicalController : BaseController

    {
        private readonly IOPDPrescriptionMedicalService _OPDPrescriptionService;
        private readonly IPrescriptionOPTemplateService _PrescriptionOPTemplateService;

        private readonly IGenericService<ServiceMaster> _serviceMasterrepository;
        private readonly IGenericService<MOpcasepaperDignosisMaster> _Dignos;
        private readonly IGenericService<MExaminationMaster> _Examination;
        private readonly IGenericService<MComplaintMaster> _MComplaintMaster;

        public OPDPrescriptionMedicalController(IOPDPrescriptionMedicalService repository, IPrescriptionOPTemplateService repository1, IGenericService<ServiceMaster> ServiceMasterrepository, IGenericService<MOpcasepaperDignosisMaster> MOpcasepaperDignosisMasterrepository, IGenericService<MExaminationMaster> MExaminationMasterrepository
            , IGenericService<MComplaintMaster> MComplaintMasterrepository)
        {
            _OPDPrescriptionService = repository;
            _PrescriptionOPTemplateService = repository1;
            _serviceMasterrepository = ServiceMasterrepository;
            _Dignos = MOpcasepaperDignosisMasterrepository;
            _Examination = MExaminationMasterrepository;
            _MComplaintMaster = MComplaintMasterrepository;
        }
        [HttpPost("GetVisitList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<GetVisitInfoListDto> GetVisitList = await _OPDPrescriptionService.GetListAsync(objGrid);
            return Ok(GetVisitList.ToGridResponse(objGrid, "GetVisitList "));
        }

        [HttpPost("PrescriptionDetailsVisitList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListP(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetailsVisitWiseListDto> GetVisitList = await _OPDPrescriptionService.GetListAsyncL(objGrid);
            return Ok(GetVisitList.ToGridResponse(objGrid, "PrescriptionDetailsVisitList "));
        }
        //List API
        [HttpGet]
        [Route("get-Service")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MServicetMasterList = await _serviceMasterrepository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service dropdown", MServicetMasterList.Select(x => new { x.ServiceName, x.ServiceId }));
        }

        //List API
        [HttpGet]
        [Route("get-Dignosis")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDignosDropdown()
        {
            var MDignosMasterList = await _Dignos.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dignosis dropdown", MDignosMasterList.Select(x => new { x.DescriptionName, x.VisitId, x.DescriptionType }));
        }

        [HttpPost("GetDignosisList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> DignsisList(GridRequestModel objGrid)
        {
            IPagedList<MOpcasepaperDignosisMaster> GetVisitList = await _OPDPrescriptionService.GetDignosisListAsync(objGrid);
            return Ok(GetVisitList.ToGridResponse(objGrid, "Get DignosisList "));
        }

        //List API
        [HttpGet]
        [Route("get-Examination")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetExaminationDropdown()
        {
            var MExaminationMasterList = await _Examination.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Examination dropdown", MExaminationMasterList.Select(x => new { x.ExaminationId, x.ExaminationDescr }));
        }
        //List API
        [HttpGet]
        [Route("get-ChiefComplaint")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetChiefComplaintDropdown()
        {
            var MChiefComplaintMasterList = await _MComplaintMaster.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ChiefComplaint dropdown", MChiefComplaintMasterList.Select(x => new { x.ComplaintId, x.ComplaintDescr }));
        }

        [HttpPost("OPRequestList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<IActionResult> OPRequestList(GridRequestModel objGrid)
        {
            IPagedList<OPRequestListDto> List = await _OPDPrescriptionService.TOprequestList(objGrid);
            return Ok(List.ToGridResponse(objGrid, "OP Request List"));
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelTPrescription obj)
        {
            TPrescription model = obj.TPrescription.MapTo<TPrescription>();
            List<TOprequestList> objTOPRequest = obj.TOPRequestList.MapTo<List<TOprequestList>>();
            List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosis = obj.MOPCasepaperDignosisMaster.MapTo<List<MOpcasepaperDignosisMaster>>();

            if (model.OpdIpdIp != 0)
            {
                model.Date = Convert.ToDateTime(obj.TPrescription.Date);
                model.CreatedBy = CurrentUserId;
                objTOPRequest.ForEach(x => { x.OpIpId = obj.TPrescription.OpdIpdIp; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });
                objmOpcasepaperDignosis.ForEach(x => { x.VisitId = obj.TPrescription.OpdIpdIp; });

                await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, objTOPRequest, objmOpcasepaperDignosis, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }
        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(ModelTPrescription obj)
        //{
        //    List<TPrescription> model = obj.TPrescription.MapTo<List<TPrescription>>();
        //    List<TOprequestList> objTOPRequest = obj.TOPRequestList.MapTo<List<TOprequestList>>();
        //    List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosis = obj.MOPCasepaperDignosisMaster.MapTo<List<MOpcasepaperDignosisMaster>>();

        //    long i = (model[0].OpdIpdIp).ToInt();


        //    if (model[0].OpdIpdIp != 0)
        //    {

        //        objTOPRequest.ForEach(x => { x.OpIpId = model[0].OpdIpdIp; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });
        //        objmOpcasepaperDignosis.ForEach(x => { x.VisitId = model[0].OpdIpdIp; });

        //        await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, objTOPRequest, objmOpcasepaperDignosis, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        //}


        
        [HttpPost("OPTemplateInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(PreTemplateModel obj)
        {
            MPresTemplateH model = obj.PrescriptionOPTemplate.MapTo<MPresTemplateH>();
            MPresTemplateD objTemplate = obj.PresTemplate.MapTo<MPresTemplateD>();
            if (obj.PrescriptionOPTemplate.PresId == 0)
            {
                model.CreatedDate = Convert.ToDateTime(model.CreatedDate);
                model.CreatedBy = CurrentUserId;
                objTemplate.Date = Convert.ToDateTime(objTemplate.Date);
                await _PrescriptionOPTemplateService.InsertAsyncSP(model, objTemplate, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPTemplate added successfully.");
        }

        [HttpPost("OPTemplateEDMX")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PrescriptionOPTemplateModel obj)
        {
            MPresTemplateH model = obj.MapTo<MPresTemplateH>();
            MPresTemplateD model1 = obj.MapTo<MPresTemplateD>();


            if (model.PresId == 0)
            {
                model.CreatedDate = Convert.ToDateTime(model.CreatedDate);
                model.CreatedBy = CurrentUserId;
                model1.Date = Convert.ToDateTime(model1.Date);
                await _PrescriptionOPTemplateService.InsertAsync(model, model1 , CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPTemplate added successfully.");
        }
    }
}
