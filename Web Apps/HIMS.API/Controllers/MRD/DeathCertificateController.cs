using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
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
    public class DeathCertificateController : BaseController
    {
        private readonly IGenericService<TDeathCertificate> _repository;

        public DeathCertificateController(IGenericService<TDeathCertificate> repository)
        {
            _repository = repository;
        }

        // 1. List API
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TDeathCertificate> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DeathCertificate List"));
        }

        // 2. Insert API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DeathCertificate", Permission = Permission.Add)]
        public async Task<ApiResponse> Insert(DeathCertificateModel obj)
        {
            if (obj.CertificateId == 0)
            {
                TDeathCertificate model = obj.MapTo<TDeathCertificate>();

                
                model.CertificateDate = AppTime.Now;
                model.CertificateTime = AppTime.Now;

                model.AddedBy = CurrentUserId;
                model.UpdatedBy = null;

                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // 3. Update API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DeathCertificate", Permission = Permission.Edit)]
        public async Task<ApiResponse> Update(DeathCertificateModel obj)
        {
            if (obj.CertificateId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            TDeathCertificate model = obj.MapTo<TDeathCertificate>();

            
            model.CertificateDate = AppTime.Now;
            model.CertificateTime = AppTime.Now;

            model.UpdatedBy = CurrentUserId;

            await _repository.Update(model, CurrentUserId, CurrentUserName, new string[1] { "AddedBy" });
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}