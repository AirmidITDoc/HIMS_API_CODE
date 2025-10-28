using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.API.Utility;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IFileUtility _FileUtility;
        private readonly IGenericService<FileMaster> _FileService;
        public FilesController(IFileUtility fileUtility, IGenericService<FileMaster> genericService)
        {
            this._FileUtility = fileUtility;
            this._FileService = genericService;
        }
        [HttpGet("get-files")]
        [Permission]
        public async Task<ApiResponse> GetFiles(int RefId, PageNames RefType)
        {
            var result = await _FileService.GetAll(x => x.RefId == RefId && x.RefType == (int)RefType);
            List<FileModel> bList = result.Select((a, index) => new FileModel()
            {
                DocName = a.DocName,
                DocSavedName = a.DocSavedName,
                Id = a.Id,
                IsDelete = false,
                RefId = a.RefId,
                RefType = (PageNames)a.RefType,
                SrNo = index + 1
            }).ToList();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", bList);
        }
        [HttpGet("get-file")]
        [Permission]
        public async Task<ActionResult> GetFile(int Id)
        {
            var result = await _FileService.GetById(x => x.Id == Id);

            // Check for null result to handle CS8602 and CS8629
            if (result == null || result.RefType == null || string.IsNullOrEmpty(result.DocSavedName))
            {
                return NotFound("File not found or invalid data.");
            }

            var refTypeDescription = ((PageNames)result.RefType).ToDescription();
            var filePath = $"{refTypeDescription}\\{result.DocSavedName}";

            var data = await _FileUtility.DownloadFile(filePath);

            // Ensure data is not null before returning the file
            if (data == null || data.Item1 == null || string.IsNullOrEmpty(data.Item2) || string.IsNullOrEmpty(data.Item3))
            {
                return NotFound("File could not be downloaded.");
            }

            return File(data.Item1, data.Item2, data.Item3);
        }
        [HttpPost("save-files")]
        [Permission]
        public async Task<ApiResponse> SaveFiles([FromForm] List<FileModel> MDoctorFiles)
        {
            List<FileMaster> Files = new();
            foreach (var item in MDoctorFiles.Where(x => x.IsDelete.HasValue && !x.IsDelete.Value && x.Id == 0))
            {
                if (item.DocName != null)
                {
                    Files.Add(new FileMaster
                    {
                        DocName = item.DocName,
                        DocSavedName = await _FileUtility.UploadFileAsync(item.Document, item.RefType.ToDescription()),
                        CreatedById = CurrentUserId,
                        Id = 0,
                        IsDelete = false,
                        RefId = item.RefId,
                        RefType = (long)item.RefType,
                        CreatedDate = DateTime.Now
                    });
                }
            }
            if (Files.Count > 0)
                await _FileService.Add(Files, CurrentUserId, CurrentUserName);
            if (MDoctorFiles.Any(x => x.Id > 0 && x.IsDelete.HasValue && x.IsDelete.Value))
            {
                List<long> Ids = new();
                foreach (var item in MDoctorFiles.Where(x => x.Id > 0 && x.IsDelete.HasValue && x.IsDelete.Value))
                {
                    Ids.Add(item.Id);
                    if (!string.IsNullOrEmpty(item.DocSavedName)) // Ensure DocSavedName is not null or empty
                    {
                        _FileUtility.RemoveFile(item.DocSavedName, item.RefType.ToDescription());
                    }
                }
                await _FileService.HardDeleteBulk(x => Ids.Contains(x.Id), CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Files are saved successfully.", Files);
        }

        [HttpPost("save-signature")]
        [Permission]
        public async Task<ApiResponse> SaveSignature([FromForm] FileModel objSignature)
        {
            FileMaster obj = new();
            if (!string.IsNullOrWhiteSpace(objSignature.Base64))
            {
                await _FileService.HardDeleteBulk(x => x.RefId == objSignature.RefId && x.RefType == (int)objSignature.RefType, CurrentUserId, CurrentUserName);
                obj = new()
                {
                    DocName = objSignature.DocName,
                    DocSavedName = _FileUtility.SaveImageFromBase64(objSignature.Base64, objSignature.RefType.ToDescription()),
                    CreatedById = CurrentUserId,
                    Id = 0,
                    IsDelete = false,
                    RefId = objSignature.RefId,
                    RefType = (long)objSignature.RefType,
                    CreatedDate = DateTime.Now
                };
                await _FileService.Add(obj, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Files are saved successfully.", obj);
        }
        [HttpGet("get-signature")]
        [Permission]
        public async Task<ApiResponse> GetSignature(int RefId, PageNames RefType)
        {
            var result = (await _FileService.GetAll(x => x.RefId == RefId && x.RefType == (int)RefType)).FirstOrDefault();

            if (result == null || result.RefType == null || string.IsNullOrEmpty(result.DocSavedName))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "");
            }

            var data = await _FileUtility.GetBase64FromFolder(((PageNames)result.RefType).ToDescription(), result.DocSavedName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { data, type = result.DocName.EndsWith("_File") ? "file" : "signature" });
        }
    }
}
