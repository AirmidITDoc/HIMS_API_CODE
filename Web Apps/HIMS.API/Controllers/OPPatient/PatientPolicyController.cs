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
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PatientPolicyController : BaseController
    {
        private readonly IGenericService<MItemTypeMaster> _repository;
        public PatientPolicyController(IGenericService<MItemTypeMaster> repository)
        {
            _repository = repository;
        }

        ////List API
        //[HttpPost]
        //[Route("[action]")]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.View)]
        //public async Task<IActionResult> List(GridRequestModel objGrid)
        //{
        //    IPagedList<TPatientPolicyInformation> PatientPolicyList = await _repository.GetAllPagedAsync(objGrid);
        //    return Ok(PatientPolicyList.ToGridResponse(objGrid, "Item Type List"));
        //}

        ////List API Get By Id
        //[HttpGet("{id?}")]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository.GetById(x => x.PatientPolicyId == id);
        //    return data.ToSingleResponse<TPatientPolicyInformation, PatientPolicyModel>("ItemType List");
        //}
        ////Add API
        //[HttpPost]
        //[Permission(PageCode = "PatientPolicy", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Post(PatientPolicyModel obj)
        //{
        //    TPatientPolicyInformation model = obj.MapTo<TPatientPolicyInformation>();
        //    //model.IsActive = true;
        //    if (obj.PatientPolicyId == 0)
        //    {
        //        model.CreatedBy = CurrentUserId;
        //        model.CreatedDate = DateTime.Now;
        //        await _repository.Add(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        //}
    }
}
