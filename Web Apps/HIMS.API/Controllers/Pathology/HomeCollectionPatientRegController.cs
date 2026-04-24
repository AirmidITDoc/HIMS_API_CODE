using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pathology;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Pathlogy;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HomeCollectionPatientRegController : BaseController
    {

        private readonly IHomeCollectionPatientRegService _IHomeCollectionPatientRegService;
        private readonly IGenericService<THomeCollectionPatientRegistartion> _repository;


        public HomeCollectionPatientRegController(IHomeCollectionPatientRegService repository, IGenericService<THomeCollectionPatientRegistartion> repository1)
        {
            _IHomeCollectionPatientRegService = repository;
            _repository = repository1;
        }
        [HttpPost("HomeCollectionPatientRegistartionList")]
        //[Permission(PageCode = "InPatient", Permission = PagePermission.View)]
        public async Task<IActionResult> salesbrowselist(GridRequestModel objGrid)
        {
            IPagedList<HomeCollectionPatientRegistartionListDto> salesbrowselist = await _IHomeCollectionPatientRegService.GetListAsync(objGrid);
            return Ok(salesbrowselist.ToGridResponse(objGrid, "HomeCollectionPatientRegistartion List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.PatientRegId == id);
            return data.ToSingleResponse<THomeCollectionPatientRegistartion, HomeCollectionPatientRegModel>("THomeCollectionPatientRegistartion");
        }

      
        [HttpPost("Insert")]
        //[Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.Add)]

        public async Task<ApiResponse> Insert(HomeCollectionPatientRegModel obj)
        {
            THomeCollectionPatientRegistartion model = obj.MapTo<THomeCollectionPatientRegistartion>();

            model.PatientRegId = 0;  

            if (obj.PatientRegId == 0)
            {
                await _IHomeCollectionPatientRegService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status500InternalServerError, "Invalid params");

            return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status200OK,  "Record added successfully.",  model.PatientRegId);
        }

        [HttpPost("HomeCollectionPatientInsert")]
        //[Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> HomeInsert(HomeCollectionPatientRegistrationModel obj)
        {
            THomeCollectionPatientRegistartion model = obj.MapTo<THomeCollectionPatientRegistartion>();
            if (obj.PatientRegId == 0)
            {
                foreach (var q in model.THomeCollectionPatientRegDetails)
                {
                  
                }
               
             await _IHomeCollectionPatientRegService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.PatientRegId);
        }
    }
}
