using Asp.Versioning;
using DocumentFormat.OpenXml.Office2010.Excel;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.API.Models.DocumentManagement;
using HIMS.API.PdfUtility;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.DocumentManagement;
using HIMS.Data.Models;
using HIMS.Services.DocumentManagement;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace HIMS.API.Controllers.DocumentManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DocumentUploadController : BaseController
    {
        private readonly IDocumentUploadService _repository;
        private readonly IFileUtility _FileUtility;
        private readonly ICategoryMergeService _merge;
        public DocumentUploadController(IDocumentUploadService repository, IFileUtility fileUtility, ICategoryMergeService categoryMergeService)
        {
            _repository = repository;
            _FileUtility = fileUtility;
            _merge = categoryMergeService;
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
        [HttpGet]
        [Route("get-admission-files")]
        [Permission(PageCode = "DocumentCategory", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetFiles(long AdmissionId, int CategoryId)
        {
            var PatientList = await _repository.GetDocumentsByCategoryAndAdmissionId(AdmissionId, CategoryId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category tree retrieved successfully.", PatientList);
        }
        [HttpGet]
        [Route("get-patient-files")]
        [Permission(PageCode = "DocumentCategory", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetPatientFiles(long PatientId)
        {
            var PatientList = await _repository.GetDocumentsByPatientId(PatientId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category tree retrieved successfully.", PatientList);
        }
        [HttpGet("get-file")]
        [Permission]
        public async Task<ActionResult> GetFile(string FileName)
        {
            var data = await _FileUtility.DownloadFile("DocumentManagement\\" + FileName);
            if (data == null || data.Item1 == null || string.IsNullOrEmpty(data.Item2) || string.IsNullOrEmpty(data.Item3))
            {
                return NotFound("File could not be downloaded.");
            }
            return File(data.Item1, data.Item2, data.Item3);
        }

        [HttpGet("merged-pdf")]
        public async Task<IActionResult> DownloadMergedPdf(int AdmissionId,int CategoryId, CancellationToken ct = default)
        {
            var PatientList = await _repository.GetDocumentsByCategoryAndAdmissionId(AdmissionId, CategoryId);
            var result = await _merge.MergeAsync(PatientList, _FileUtility.GetFolderPath("DocumentManagement"), ct);
            if (result == null)
                return NotFound(new { message = "No mergeable files found in this category." });

            Response.Headers["X-Skipped-Count"] = result.SkippedFiles.Count.ToString();
            Response.Headers["Access-Control-Expose-Headers"] = "X-Skipped-Count, Content-Disposition";

            // DeleteOnClose removes the temp file after the response is sent
            var stream = new FileStream(result.PdfPath, FileMode.Open, FileAccess.Read, FileShare.Read,
                81920, FileOptions.DeleteOnClose | FileOptions.Asynchronous);

            return File(stream, "application/pdf", $"category-{CategoryId}.pdf");
        }
    }
}
