using Asp.Versioning;
using DocumentFormat.OpenXml.Office2010.Excel;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.API.Models.DocumentManagement;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.Models;
using HIMS.Services.DocumentManagement;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.DocumentManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DocumentUploadController : BaseController
    {
        private readonly IDocumentUploadService _repository;
        private readonly IFileUtility _FileUtility;
        public DocumentUploadController(IDocumentUploadService repository, IFileUtility fileUtility)
        {
            _repository = repository;
            _FileUtility = fileUtility;
        }

        //List API
        [HttpGet]
        [Route("search-patient")]
        [Permission(PageCode = "DocumentCategory", Permission = PagePermission.View)]
        public async Task<ApiResponse> SearchPatient(string Keyword)
        {
            var PatientList = await _repository.SearchRegistration(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category tree retrieved successfully.", PatientList);
        }
        [HttpGet]
        [Route("patient-admissions")]
        [Permission(PageCode = "DocumentCategory", Permission = PagePermission.View)]
        public async Task<ApiResponse> SearchPatient(long PatientId)
        {
            var RegList = await _repository.GetRegistrationsByPatientId(PatientId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category tree retrieved successfully.", RegList.Select(x => new { x.AdmissionDate, x.Ipdno, x.Ipnumber, x.RegId, x.PatientTypeId, x.AdmissionId }));
        }

        [HttpPost("upload-files")]
        [Permission]
        public async Task<ApiResponse> SaveFiles([FromForm] List<DocumentFileModel> model)
        {
            List<DocumentFile> Files = new();
            foreach (var item in model.Where(x => x.Id == 0))
            {
                if (item.OrgFileName != null)
                {
                    item.SavedFileName = await _FileUtility.UploadFileAsync(item.Document, "DocumentManagement");
                }
                Files.Add(item.MapTo<DocumentFile>());
            }
            await _repository.Add(Files, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Files are saved successfully.", Files);
        }
        [HttpGet]
        [Route("get-files")]
        [Permission(PageCode = "DocumentCategory", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetFiles()
        {
            var PatientList = await _repository.GetAllDocuments();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category tree retrieved successfully.", PatientList);
        }
    }
}
