using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pathology;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class HomeCollectionPatientRegController : BaseController
    {

        private readonly IHomeCollectionPatientRegService _IHomeCollectionPatientRegService;
        private readonly IGenericService<THomeCollectPatientRegistartion> _repository;


        public HomeCollectionPatientRegController(IHomeCollectionPatientRegService repository, IGenericService<THomeCollectPatientRegistartion> repository1)
        {
            _IHomeCollectionPatientRegService = repository;
            _repository = repository1;



        }
        
        [HttpPost("Insert")]
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

       
    }
}
