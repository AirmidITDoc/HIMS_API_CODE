using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Pathology;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.Nursing;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyController : BaseController
    {
        private readonly IPathlogySampleCollectionService _IPathlogySampleCollectionService;
        private readonly ILabRequestService _ILabRequestService;
        private readonly IPathlogyService _IPathlogyService;
        private readonly IGenericService<MTemplateMaster> _radiorepository;
        private readonly IGenericService<MDoctorNotesTemplateMaster> _radiorepository1;
        private readonly IGenericService<MNursingTemplateMaster> _radiorepository2;

        public PathologyController(IPathlogySampleCollectionService repository, ILabRequestService repository1, IPathlogyService repository2, IGenericService<MTemplateMaster> pathrepository, IGenericService<MDoctorNotesTemplateMaster> pathrepository1, IGenericService<MNursingTemplateMaster> pathrepository2)
        {
            _IPathlogySampleCollectionService = repository;
            _ILabRequestService = repository1;
            _IPathlogyService = repository2;
            _radiorepository = pathrepository;
            _radiorepository1 = pathrepository1;
            _radiorepository2 = pathrepository2;
        }

       
        [HttpGet("search-pathologistdoctor")]
        public ApiResponse SearchPatientNew()
        {
            var data = _IPathlogyService.SearchPatient();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathologist Doctor List", data);
        }

        [HttpPost("PathologyPatientTestList")]
        [Permission(PageCode = "Pathology", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientList(GridRequestModel objGrid)
        {
            IPagedList<PathPatientTestListDto> PatientList = await _IPathlogyService.GetListAsync(objGrid);
            return Ok(PatientList.ToGridResponse(objGrid, "PathologyPatientTestList "));
        }

        [HttpPost("PathologyTestList")]
        [Permission(PageCode = "Pathology", Permission = PagePermission.View)]
        public async Task<IActionResult> PathResultEntryList(GridRequestModel objGrid)
        {
            IPagedList<PathResultEntryListDto> PathResultEntryList = await _IPathlogyService.PathResultEntry(objGrid);
            return Ok(PathResultEntryList.ToGridResponse(objGrid, "PathResultEntryList"));

        }


        [HttpPost("InsertResultEntry")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathologyResultModel obj)
        {
            List<TPathologyReportDetail> model = obj.PathologyResult.MapTo<List<TPathologyReportDetail>>();
            TPathologyReportHeader objTPathology = obj.PathologyReport.MapTo<TPathologyReportHeader>();
            if (model.Count > 0)
            {
                await _IPathlogyService.InsertAsyncResultEntry(model, objTPathology, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("pathologyOutsourceUpdate/{id:int}")]
        [Permission(PageCode = "Pathology", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathologyResultUpdate obj)
        {
            TPathologyReportHeader model = obj.MapTo<TPathologyReportHeader>();

            if (obj.PathReportId == 0)

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPathlogyService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPost("PathPrintResultentryInsert")]
        [Permission(PageCode = "Pathology", Permission = PagePermission.Add)]
        public ApiResponse Insert(PathPrintResultentry obj)
        {
            List<TempPathReportId> model = obj.PathPrintResultEntry.MapTo<List<TempPathReportId>>();
            if (model.Count > 0)
            {
                _IPathlogyService.InsertPathPrintResultentry(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathPrintResultentry  added successfully.");
        }




        [HttpPost("PathologyTemplateSave")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathologyTemplatesModel obj)
        {
            TPathologyReportTemplateDetail model = obj.PathologyReportTemplate.MapTo<TPathologyReportTemplateDetail>();
            TPathologyReportHeader objPathologyReportHeader = obj.PathologyReportHeader.MapTo<TPathologyReportHeader>();
            if (model.PathReportTemplateDetId == 0)
            {
                objPathologyReportHeader.ReportDate = Convert.ToDateTime(objPathologyReportHeader.ReportDate);
                objPathologyReportHeader.ReportTime = Convert.ToDateTime(objPathologyReportHeader.ReportTime);
                await _IPathlogyService.InsertAsyncResultEntry1(model, objPathologyReportHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }


        [HttpGet]
        [Route("get-PathologyTemplates")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MMasterList = await _radiorepository.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Template dropdown", MMasterList.Select(x => new { x.TemplateId, x.TemplateName, x.TemplateDesc }));
        }
        [HttpGet]
        [Route("get-DoctorNotesTemplateMaster")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown1()
        {
            var MMasterList = await _radiorepository1.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorNotes Template dropdown", MMasterList.Select(x => new { x.DocNoteTempId, x.DocsTempName, x.TemplateDesc }));
        }
        [HttpGet]
        [Route("get-NursingTemplateMaster")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown2()
        {
            var MMasterList = await _radiorepository2.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorNotes Template dropdown", MMasterList.Select(x => new { x.NursingId, x.NursTempName, x.TemplateDesc }));
        }


        [HttpPost("PathResultentryrollback")]
        [Permission(PageCode = "Pathology", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Delete(PathReportModel obj)
        {
            TPathologyReportDetail Model = obj.MapTo<TPathologyReportDetail>();

            if (obj.PathReportID != 0)
            {

                //   Model.AddedBy = CurrentUserId;
                await _IPathlogyService.DeleteAsync(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test has been rolled back successfully.");
        }

        [HttpPost("Verify")]
        [Permission(PageCode = "Pathology", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(PathologyVerifyModel obj)
        {
            TPathologyReportHeader model = obj.MapTo<TPathologyReportHeader>();
            if (obj.PathReportId != 0)
            {
                model.IsVerifySign = true;
                model.IsVerifyedDate = DateTime.Now.Date;

                await _IPathlogyService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record verify successfully.");
        }
    }
}
