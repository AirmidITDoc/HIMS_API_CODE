using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TarrifMasterController : BaseController
    {
        private readonly IGenericService<TariffMaster> _repository;
        public TarrifMasterController(IGenericService<TariffMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TariffMaster> TariffMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TariffMasterList.ToGridResponse(objGrid, "Department List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TariffId == id);
            return data.ToSingleResponse<TariffMaster, TarifMasterModel>("PatientType");
        }
        ////List API Get By Id
        //[HttpGet("{id?}")]
        //[Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository.GetById(x => x.DepartmentId == id);
        //    return data.ToSingleResponse<MDepartmentMaster, DepartmentMasterModel>("DepartmentMaster Type");
        //}


    }
}
