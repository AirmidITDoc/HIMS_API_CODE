using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NursingConsentController: BaseController
    {
        private readonly INursingConsentService _NursingConsentService;

        private readonly IGenericService<TConsentInformation> _repository;
      
        public NursingConsentController(INursingConsentService repository,  IGenericService<TConsentInformation> repository4)
        {
            _repository = repository4;
            _NursingConsentService = repository;

        }

        //[HttpPost("InsertEDMX")]
        ////    [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(ConsentInformationModel obj)
        //{
        //    TConsentInformation model = obj.MapTo<TConsentInformation>();
        //    if (obj.ConsentId == 0)
        //    {
        //        model.CreatedDatetime = DateTime.Now;
        //        model.CreatedBy = CurrentUserId;
        //        //model.AddedBy = CurrentUserId;
        //        //model.IsActive = true;
        //        await _NursingConsentService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  added successfully.");
        //}

        [HttpPost("InsertConsent")]
        //   [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse>insert(ConsentInformationModel obj)
        {
            TConsentInformation Model = obj.MapTo<TConsentInformation>();
            //    List<AddCharge> Models = obj.AddCharge.MapTo<List<AddCharge>>();

            if (obj.ConsentId == 0)
            {
                Model.CreatedDatetime = DateTime.Now;
                Model.CreatedBy = CurrentUserId;
                ///    Model.AddedBy = CurrentUserId;
                await _NursingConsentService.InsertAsync(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ConsentInformation Added successfully.");
        }


        [HttpPut("UpdateConsent")]
        //   [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(UpdateConsentInformationModel obj)
        {
            TConsentInformation Model = obj.MapTo<TConsentInformation>();
            //    List<AddCharge> Models = obj.AddCharge.MapTo<List<AddCharge>>();

            if (obj.ConsentId != 0)
            {
                Model.ModifiedDateTime = DateTime.Now;
                Model.ModifiedBy = CurrentUserId;
                ///    Model.AddedBy = CurrentUserId;
                await _NursingConsentService.UpdateAsync(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ConsentInformation Update successfully.");
        }

        [HttpPost("ConsentpatientInfoList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> ConsentpatientInfoList(GridRequestModel objGrid)
        {
            IPagedList<ConsentpatientInfoListDto> ConsentpatientInfoList = await _NursingConsentService.ConsentpatientInfoList(objGrid);
            return Ok(ConsentpatientInfoList.ToGridResponse(objGrid, "ConsentpatientInfo List"));
        }
    }
}
