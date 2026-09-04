using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.MRD;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
using HIMS.Services.Dashboard;
using HIMS.Services.MRD;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.MRD
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DeathCertificateController : BaseController
    {
        private readonly IGenericService<TDeathCertificate> _repository;
        private readonly IDeathCertificateService _IDeathCertificateService;

        public DeathCertificateController(IDeathCertificateService repository, IGenericService<TDeathCertificate> repository1)
        {
            _repository = repository1;
            _IDeathCertificateService = repository;
        }

        // 1. LIST API (Pagination & Filtering)
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            if (objGrid != null)
            {
                objGrid.SortField = string.IsNullOrEmpty(objGrid.SortField) ? string.Empty : objGrid.SortField;
                objGrid.Filters ??= new List<SearchGrid>();
            }

            IPagedList<TDeathCertificate> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DeathCertificate List"));
        }

        [HttpPost("CertificateList")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> CertificateList(GridRequestModel objGrid)
        {
            IPagedList<CertifiCateListDto> CertificateList = await _IDeathCertificateService.CertificateListAsync(objGrid);
            return Ok(CertificateList.ToGridResponse(objGrid, "CertificateList"));
        }

        // 2. GET BY ID API (Fetch single record)
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ApiResponse> GetById(int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid Certificate ID");
            }

            var record = await _repository.GetById(x => x.CertificateId == id);

            if (record == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "Record not found");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record fetched successfully.", record);
        }

        // 3. INSERT (CREATE) API
        [HttpPost]
        [Route("[action]")]
        public async Task<ApiResponse> Insert(DeathCertificateModel obj)
        {
            if (obj == null || obj.CertificateId != 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params for insertion");
            }

            TDeathCertificate model = obj.MapTo<TDeathCertificate>();

            model.CertificateDate = AppTime.Now.Date;
            model.CertificateTime = AppTime.Now;
            model.AddedBy = CurrentUserId;

            await _IDeathCertificateService.InsertAsync(model, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.CertificateId);
        }

        // 4. UPDATE API 
        [HttpPut]
        [Route("[action]/{id}")]
        //[Permission(PageCode = "DeathCertificate", Permission = Permission.Edit)]
        public async Task<ApiResponse> Update(int id, DeathCertificateModel obj)
        {
            if (id <= 0 || obj == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params for updation");
            }

            obj.CertificateId = id;
            TDeathCertificate model = obj.MapTo<TDeathCertificate>();

            model.CertificateDate = AppTime.Now.Date;
            model.CertificateTime = AppTime.Now;
            model.UpdatedBy = CurrentUserId;

          
            await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CertificateNo", "AddedBy" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}