using Asp.Versioning;
using DocumentFormat.OpenXml.Office2010.Excel;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PatientAbhaInformationController : BaseController
    {
        private readonly IPatientAbhaInformationService _IPatientAbhaInformationService;
        private readonly IGenericService<TPatientAbhaInformation> _repository;


        public PatientAbhaInformationController(IPatientAbhaInformationService repository, IGenericService<TPatientAbhaInformation> repository1)
        {
            _IPatientAbhaInformationService = repository;
            _repository = repository1;


        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.AbhaTranId == id);
            return data.ToSingleResponse<TPatientAbhaInformation, PatientAbhaInformationModel>("PatientType");
        }

        [HttpGet("ByAbhaNumber/{AbhaNumber?}")]
        //[Permission]
        public async Task<ApiResponse> GetListByAbhaNumber(string AbhaNumber)
        {
            if (string.IsNullOrWhiteSpace(AbhaNumber))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "ABHA Number is required.");
            }

            var data = await _repository.GetAll(x => x.AbhaNumber == AbhaNumber);

            if (data == null || !data.Any())
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound,"No data found." );
            }

            var result = data.Select(x => new PatientAbhaInformationModel
            {
                AbhaTranId = x.AbhaTranId,
                RegId = x.RegId,
                AbhaNumber = x.AbhaNumber,
                AbhaFullName = x.AbhaFullName,
                AbhaAddress = x.AbhaAddress,
                Gender = x.Gender,
                YearOfBirth = x.YearOfBirth,
                Verified = x.Verified,
                VerifiedDateTime = x.VerifiedDateTime,
                CreatedBy = x.CreatedBy
            }).ToList();

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Data found.",result);
        }
        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(PatientAbhaInformationModel obj)
        {
            TPatientAbhaInformation model = obj.MapTo<TPatientAbhaInformation>();
            if (obj.AbhaTranId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.IsActive = true;
                await _IPatientAbhaInformationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
       
        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(PatientAbhaInformationUpdateModel obj)
        {
            TPatientAbhaInformation model = obj.MapTo<TPatientAbhaInformation>();
            if (obj.AbhaTranId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.IsActive = true;
                await _IPatientAbhaInformationService.UpdateAsync(model, CurrentUserId, CurrentUserName);

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
