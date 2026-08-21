using Asp.Versioning;
using DocumentFormat.OpenXml.Office2010.Excel;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
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
        public DocumentUploadController(IDocumentUploadService repository)
        {
            _repository = repository;
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
        ////List API Get By Id
        //[HttpGet("{id?}")]
        //[Permission(PageCode = "DocumentCategory", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository.GetById(x => x.Id == id);
        //    return data.ToSingleResponse<DocumentCategory, DocumentCategory>("DocumentCategory");
        //}
        ////Add API
        //[HttpPost]
        //[Permission(PageCode = "DocumentCategory", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Post(DocumentCategory obj)
        //{
        //    DocumentCategory model = obj.MapTo<DocumentCategory>();
        //    model.IsActive = true;
        //    if (obj.Id == 0)
        //    {
        //        await _repository.Add(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        //}
        ////Edit API
        //[HttpPut("{id:int}")]
        //[Permission(PageCode = "DocumentCategory", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(DocumentCategory obj)
        //{
        //    if (obj.Id == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        var data = await _repository.GetById(x => x.Id == obj.Id);
        //        if (data == null)
        //            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Record not found.");

        //        data.Icon = obj.Icon;
        //        data.DocCategory = obj.DocCategory;
        //        await _repository.Update(data, CurrentUserId, CurrentUserName,null);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}
        ////Delete API
        //[HttpDelete]
        //[Permission(PageCode = "DocumentCategory", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> Delete(int Id)
        //{
        //    DocumentCategory model = await _repository.GetById(x => x.Id == Id);
        //    if ((model?.Id ?? 0) > 0)
        //    {
        //        model.IsDeleted = true;
        //        await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //}
    }
}
