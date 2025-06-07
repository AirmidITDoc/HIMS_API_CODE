using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OutPatientController : BaseController
    {
        private readonly IRegistrationService _IRegistrationService;
        private readonly IGenericService<Registration> _repository;
        public OutPatientController(IRegistrationService repository, IGenericService<Registration> repository1)
        {
            _IRegistrationService = repository;
            _repository = repository1;
        }

        [HttpPost("RegistrationList")]
        [Permission(PageCode = "Registration", Permission = PagePermission.View)]
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
            return data.ToSingleResponse<Registration, RegistrationModel>("Registration");
        }

        [HttpPost("RegistrationInsert")]
        [Permission(PageCode = "Registration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RegistrationModel obj)
        {
            Registration model = obj.MapTo<Registration>();
            if (obj.RegId == 0)
            {
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.RegTime = Convert.ToDateTime(obj.RegTime);
                model.DateofBirth = Convert.ToDateTime(obj.DateOfBirth);
                model.AddedBy = CurrentUserId;
                await _IRegistrationService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }

        [HttpPost("RegistrationUpdate")]
        [Permission(PageCode = "Registration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(RegistrationModel obj)
        {
            Registration model = obj.MapTo<Registration>();
            if (obj.RegId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.RegDate = Convert.ToDateTime(obj.RegDate);
                model.RegTime = Convert.ToDateTime(obj.RegTime);
                await _IRegistrationService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model);
        }
        [HttpGet("auto-complete")]
        [Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetAutoComplete(string Keyword)
        {
            var data = await _IRegistrationService.SearchRegistration(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Registration Data.", data.Select(x => new { 
                Text = x.FirstName + " " + x.LastName + " | " + x.RegNo + " | " + x.Mobile, 
                Value = x.Id ,
                RegNo = x.RegNo,
                MobileNo = x.MobileNo,
                AgeYear = x.AgeYear ,
                AgeMonth = x.AgeMonth ,
                AgeDay = x.AgeDay ,
                PatientName = x.FirstName + " " + x.LastName
            }));
        }

    }
}
