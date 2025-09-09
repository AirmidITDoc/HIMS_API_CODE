using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.OutPatient.TPrescriptionModel;

namespace HIMS.API.Controllers.OPPatient
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
        private readonly HIMSDbContext _context;
        private readonly INotificationUtility _notificationUtility;
        private readonly IVisitDetailsService _visitDetailsService;
        public OPDPrescriptionMedicalController(IOPDPrescriptionMedicalService repository, IPrescriptionOPTemplateService repository1, IGenericService<ServiceMaster> ServiceMasterrepository, 
            IGenericService<MOpcasepaperDignosisMaster> MOpcasepaperDignosisMasterrepository, IGenericService<MExaminationMaster> MExaminationMasterrepository
            , IGenericService<MComplaintMaster> MComplaintMasterrepository, HIMSDbContext HIMSDbContext, INotificationUtility notificationUtility, IVisitDetailsService visitDetailsService)
        {
            _OPDPrescriptionService = repository;
            _PrescriptionOPTemplateService = repository1;
            _serviceMasterrepository = ServiceMasterrepository;
            _Dignos = MOpcasepaperDignosisMasterrepository;
            _Examination = MExaminationMasterrepository;
            _MComplaintMaster = MComplaintMasterrepository;
            _context = HIMSDbContext;
            _notificationUtility = notificationUtility;
            _visitDetailsService = visitDetailsService;
        }
        [HttpPost("GetVisitList")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<GetVisitInfoListDto> GetVisitList = await _OPDPrescriptionService.GetListAsync(objGrid);
            return Ok(GetVisitList.ToGridResponse(objGrid, "GetVisitList "));
        }
        [HttpPost("OPRequestListFromEMR")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<OPRequestListFromEMRDto> RequestListFromEMRD = await _OPDPrescriptionService.GetListAsyncE(objGrid);
            return Ok(RequestListFromEMRD.ToGridResponse(objGrid, "OPRequestListFromEMR "));
        }
        [HttpPost("GetPrevVisitDiagnosisList")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> ListG(GridRequestModel objGrid)
        {
            IPagedList<GetPrevVisitDiagnosisListDto> GetPrevVisitDiagnosisList = await _OPDPrescriptionService.VGetListAsync(objGrid);
            return Ok(GetPrevVisitDiagnosisList.ToGridResponse(objGrid, "GetPrevVisitDiagnosisList "));
        }


        [HttpPost("PrescriptionDetailsVisitList")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> ListP(GridRequestModel objGrid)
        {
            IPagedList<PrescriptionDetailsVisitWiseListDto> GetVisitList = await _OPDPrescriptionService.GetListAsyncL(objGrid);
            return Ok(GetVisitList.ToGridResponse(objGrid, "PrescriptionDetailsVisitList "));
        }

        [HttpPost("getlabifnormationList")]
        //[Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> ListL(GridRequestModel objGrid)
        {
            IPagedList<GetLabInformationListDto> getlabifnormationList = await _OPDPrescriptionService.GetListAsynL(objGrid);
            return Ok(getlabifnormationList.ToGridResponse(objGrid, "getlabifnormationList "));
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dignosis dropdown", MDignosMasterList.Select(x => new { x.Id, x.DescriptionName, x.VisitId, x.DescriptionType }));
        }

        [HttpPost("GetDignosisList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> DignsisList(GridRequestModel objGrid)
        {
            IPagedList<MOpcasepaperDignosisMaster> GetVisitList = await _OPDPrescriptionService.GetDignosisListAsync(objGrid);
            return Ok(GetVisitList.ToGridResponse(objGrid, "Get DignosisList "));
        }

        [HttpPost("OPRtrvDignosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<IActionResult> OPDignosisList(GridRequestModel objGrid)
        {
            IPagedList<OPrtrvDignosisListDto> List = await _OPDPrescriptionService.TDignosisrRtrvList(objGrid);
            return Ok(List.ToGridResponse(objGrid, "OP Rtrv Dignosis  List"));
        }

      
        [HttpGet("GetDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDiagnosisList(string descriptionType)
        {
            var result = await _OPDPrescriptionService.GetDignosisListAsync(descriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetOPrtrvDignosisList", result);
        }

        [HttpPost("OPPrescriptionTemplateList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionTemplateDetailsList(GridRequestModel objGrid)
        {
            IPagedList<getPrescriptionTemplateDetailsListDto> List = await _OPDPrescriptionService.TemplateDetailsList(objGrid);
            return Ok(List.ToGridResponse(objGrid, "getPrescriptionTemplateDetails List"));
        }


        //List API
        [HttpGet]
        [Route("get-Examination")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetExaminationDropdown()
        {
            var MExaminationMasterList = await _Examination.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Examination dropdown", MExaminationMasterList.Select(x => new { x.ExaminationId, x.ExaminationDescr }));
        }
        //List API
        [HttpGet]
        [Route("get-ChiefComplaint")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetChiefComplaintDropdown()
        {
            var MChiefComplaintMasterList = await _MComplaintMaster.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ChiefComplaint dropdown", MChiefComplaintMasterList.Select(x => new { x.ComplaintId, x.ComplaintDescr }));
        }

        [HttpPost("OPRequestList")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.View)]
        public async Task<IActionResult> OPRequestList(GridRequestModel objGrid)
        {
            IPagedList<OPRequestListDto> List = await _OPDPrescriptionService.TOprequestList(objGrid);
            return Ok(List.ToGridResponse(objGrid, "OP Request List"));
        }

        [HttpPost("PrescriptionInsertSP")]
        //[Permission(PageCode = "Prescription", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelTPrescription obj)
        {
            List<TPrescription> model = obj.TPrescription.MapTo<List<TPrescription>>();
            VisitDetail model1 = obj.VisitDetails.MapTo<VisitDetail>();
            List<TOprequestList> objTOPRequest = obj.TOPRequestList.MapTo<List<TOprequestList>>();
            List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosis = obj.MOPCasepaperDignosisMaster.MapTo<List<MOpcasepaperDignosisMaster>>();

            var VisitId = model1.VisitId;

            if (model.Count > 0)
            {
                await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, model1, objTOPRequest, objmOpcasepaperDignosis, CurrentUserId, CurrentUserName);


                //get patient details
                var objPatient = await _visitDetailsService.PatientByVisitId(VisitId);

                // Get all UserIds for StoreId = 2
                var userIds = await _context.LoginManagers
                    .Where(x => x.StoreId == 2 && x.IsActive == true)
                    .Select(x => x.UserId)
                    .Distinct()
                    .ToListAsync();
                foreach (var userId in userIds)
                {
                    // Added by vimal on 06/05/25 for testing - binding notification on bell icon of layout... later team can change..
                    //await _notificationUtility.SendNotificationAsync("OP | EMR Request for Billing", $"{objPatient.RegNo} | {objPatient.FirstName} {objPatient.LastName}", $"opd/appointment?Mode=Bill&Id={model1.VisitId}", userId);
                    //shilpa modified 09-09-2025
                    await _notificationUtility.SendNotificationAsync("OP | EMR Request for Billing", $"{objPatient?.RegNo ?? "N/A"} {objPatient?.FirstName ?? ""} {objPatient?.LastName ?? ""}", $"opd/appointment?Mode=Bill&Id={model1.VisitId}", userId);
                }

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            //if (model.OpdIpdIp != 0)
            //{
            //    model.Date = Convert.ToDateTime(obj.TPrescription.Date);
            //    model.CreatedBy = CurrentUserId;
            //    objTOPRequest.ForEach(x => { x.OpIpId = obj.TPrescription.OpdIpdIp; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });
            //    objmOpcasepaperDignosis.ForEach(x => { x.VisitId = obj.TPrescription.OpdIpdIp; });

            //    await _OPDPrescriptionService.InsertPrescriptionAsyncSP(model, objTOPRequest, objmOpcasepaperDignosis, CurrentUserId, CurrentUserName);
            //}
            //else
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        
        //Edit API
        [HttpPut("PrescriptionEdit/{id:int}")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(UpdatePrescriptionModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId != 0)
            {
                model.PrecriptionId = obj.PrecriptionId;
                model.ModifiedBy = CurrentUserId;
                model.Date = DateTime.Now;
                await _OPDPrescriptionService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  update successfully.");
        }
        [HttpPut("GenericEdit/{id:int}")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Update(UpdatePrescription obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
            if (obj.PrecriptionId != 0)
            {
                model.PrecriptionId = obj.PrecriptionId;
                model.ModifiedBy = CurrentUserId;
                model.Date = DateTime.Now;
                await _OPDPrescriptionService.UpdateAsyncGeneric(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  update successfully.");
        }



        [HttpPost("OPTemplateInsert")]
        [Permission(PageCode = "Prescription", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(PreTemplateModel obj)
        {
            MPresTemplateH model = obj.PrescriptionOPTemplate.MapTo<MPresTemplateH>();
            List<MPresTemplateD> objTemplate = obj.PresTemplate.MapTo<List<MPresTemplateD>>();
            if (obj.PrescriptionOPTemplate.PresId == 0)
            {
                model.CreatedDate = Convert.ToDateTime(model.CreatedDate);
                model.CreatedBy = CurrentUserId;
                objTemplate.ForEach(x => { x.PresId = obj.PrescriptionOPTemplate.PresId; });

                await _PrescriptionOPTemplateService.InsertAsyncSP(model, objTemplate, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

    }
}
