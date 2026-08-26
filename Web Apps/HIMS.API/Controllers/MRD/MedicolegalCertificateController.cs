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
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.MRD
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MedicolegalCertificateController : BaseController
    {
        private readonly IGenericService<TMedicolegalCertificate> _repository;
        public MedicolegalCertificateController(IGenericService<TMedicolegalCertificate> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TMedicolegalCertificate> MedicolegalCertificateList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MedicolegalCertificateList.ToGridResponse(objGrid, "MedicolegalCertificate List"));
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
        public async Task<ApiResponse> Post(MedicolegalCertificateModel obj)
        {
            TMedicolegalCertificate model = obj.MapTo<TMedicolegalCertificate>();
            //model.IsActive = true;
            if (obj.DocId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.Mlcdate = AppTime.Now;
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(MedicolegalCertificateModel obj)
        {
            TMedicolegalCertificate model = obj.MapTo<TMedicolegalCertificate>();
            if (obj.DocId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[] { "AddedBy", "Mlcdate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
    }
}
