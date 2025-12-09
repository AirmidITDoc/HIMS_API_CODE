using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OutPatientController : BaseController
    {
        private readonly IRegistrationService _IRegistrationService;
        private readonly IGenericService<Registration> _repository;
        private readonly IFileUtility _FileUtility;

        public OutPatientController(IRegistrationService repository, IGenericService<Registration> repository1, IFileUtility fileUtility)
        {
            _IRegistrationService = repository;
            _repository = repository1;
            _FileUtility = fileUtility;
        }

        [HttpPost("RegistrationList")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RegistrationListDto> RegistrationList = await _IRegistrationService.GetListAsync(objGrid);
            return Ok(RegistrationList.ToGridResponse(objGrid, "Registration List"));
        }


        [HttpGet("{id?}")]
        [Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            var data = await _repository.GetById(x => x.RegId == id);
            return data.ToSingleResponse<Registration, RegistrationModel1>("Registration");
        }

        [HttpPost("RegistrationInsert")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RegistrationModel obj)
        {
            Registration model = obj.MapTo<Registration>();
            if (obj.RegId == 0)
            {
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.RegTime = Convert.ToDateTime(obj.RegTime);
                model.DateofBirth = Convert.ToDateTime(obj.DateofBirth);
                model.AddedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                await _IRegistrationService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }

        // Create  by Ashutosh 12 Jun 2025
        [HttpPost("InsertEDMX")]
        //  [Permission(PageCode = "Registration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(RegistrationModel obj)
        {

            if (!string.IsNullOrWhiteSpace(obj.Photo))
                obj.Photo = _FileUtility.SaveImageFromBase64(obj.Photo, "Persons\\Photo");
            Registration model = obj.MapTo<Registration>();
            if (obj.RegId == 0)
            {
                //model.CreatedDate = DateTime.Now;
                //model.CreatedBy = CurrentUserId;
                model.AddedBy = CurrentUserId;
                //model.IsActive = true;
                await _IRegistrationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPost("RegistrationUpdate")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(RegistrationModel obj)
        {
            Registration model = obj.MapTo<Registration>();
            if (obj.RegId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.RegTime = Convert.ToDateTime(obj.RegTime);
                model.UpdatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IRegistrationService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model);
        }
        [HttpGet("auto-complete")]
        [Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAutoComplete(string Keyword)
        {
            var data = await _IRegistrationService.SearchRegistration(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Registration Data.", data.Select(x => new
            {
                Text = x.FirstName + " " + x.LastName + " | " + x.RegNo + " | " + x.Mobile,
                Value = x.Id,
                RegNo = x.RegNo,
                MobileNo = x.MobileNo,
                AgeYear = x.AgeYear,
                AgeMonth = x.AgeMonth,
                AgeDay = x.AgeDay,
                PatientName = x.FirstName + " " + x.MiddleName + " " + x.LastName,     
                EmailId = x.EmailId,
                RegId =x.RegId,
                AadharCardNo = x.AadharCardNo

            }));
        }
      
        [HttpPut("UpdateReg{id:int}")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> BilldatetimeUpdate(RegistrationUpdate obj)
        {
            Registration model = obj.MapTo<Registration>();
            if (obj.RegId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IRegistrationService.RegUpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        // Create  by Ashutosh 12 Jun 2025
        [HttpGet("get-file")]
        public ApiResponse DownloadFiles(string FileName)
        {
            var data = _FileUtility.GetBase64FromFolder("Persons\\Photo", FileName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "File", data.Result);
        }

    }
}
