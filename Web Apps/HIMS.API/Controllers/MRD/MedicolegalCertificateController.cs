using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.MRD;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using HIMS.Services.MRD;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.MRD
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MedicolegalCertificateController : BaseController
    {
        private readonly IGenericService<TMedicolegalCertificate> _repository;
        private readonly IMedicolegalCertificateService _IMedicolegalCertificateService;


        public MedicolegalCertificateController(IGenericService<TMedicolegalCertificate> repository, IMedicolegalCertificateService IMedicolegalCertificateService)
        {
            _repository = repository;
            _IMedicolegalCertificateService = IMedicolegalCertificateService;
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
            var data = await _repository.GetById(x => x.DocId == id);
            return data.ToSingleResponse<TMedicolegalCertificate, MedicolegalCertificateModel>("MedicolegalCertificate");
        }
        //Add API
        [HttpPost]
        //[Permission]
        //public async Task<ApiResponse> Post(MedicolegalCertificateModel obj)
        //{
        //    TMedicolegalCertificate model = obj.MapTo<TMedicolegalCertificate>();
        //    //model.IsActive = true;
        //    if (obj.DocId == 0)
        //    {
        //        model.AddedBy = CurrentUserId;
        //        model.Mlcdate = AppTime.Now;
        //        model.Mlctime = AppTime.Now;
        //        await _repository.Add(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.", model);
        //}

      
        //Add API
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(MedicolegalCertificateModel obj)
        {
            TMedicolegalCertificate model = obj.MapTo<TMedicolegalCertificate>();
            TMlcinformation mlcInfo = obj.MapTo<TMlcinformation>();
            if (obj.DocId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.Mlcdate = AppTime.Now;
                model.Mlctime = AppTime.Now;
                await _IMedicolegalCertificateService.InsertAsync(model, mlcInfo, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }
        ////Edit API
        //[HttpPut("{id:int}")]
        ////[Permission]
        //public async Task<ApiResponse> Edit(MedicolegalCertificateModel obj)
        //{
        //    TMedicolegalCertificate model = obj.MapTo<TMedicolegalCertificate>();
        //    if (obj.DocId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.UpdatedBy = CurrentUserId;
        //        model.Mlcdate = AppTime.Now;
        //        model.Mlctime = AppTime.Now;
        //        await _repository.Update(model, CurrentUserId, CurrentUserName, new string[] { "AddedBy", "Mlcdate" });
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.", model);
        //}

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(long id, MedicolegalCertificateUpdateModel obj)
        {
            if (obj.DocId == 0 || obj.DocId != id)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            TMedicolegalCertificate model = obj.MapTo<TMedicolegalCertificate>();
            TMlcinformation mlcInfo = obj.MapTo<TMlcinformation>();
            model.UpdatedBy = CurrentUserId;

            await _IMedicolegalCertificateService.UpdateAsync(model, mlcInfo, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model);
        }
    }
}
