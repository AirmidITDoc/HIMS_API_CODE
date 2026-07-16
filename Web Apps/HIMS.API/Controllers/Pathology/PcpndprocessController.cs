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
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.OTManagment;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PcpndprocessController : BaseController
    {

        private readonly IPcpndprocesService _IPcpndprocesService;
      
        public PcpndprocessController(IPcpndprocesService repository)
        {
            _IPcpndprocesService = repository;
          
        }
       
        [HttpPost("RadioPcpndtList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RadioPcpndtListDto> RadioPcpndtList = await _IPcpndprocesService.GetListAsync(objGrid);
            return Ok(RadioPcpndtList.ToGridResponse(objGrid, "RadioPcpndt List"));
        }

        [HttpGet("search-RadiologistDoctor")]
        //[Permission]
        public ApiResponse SearchPatientNew()
        {
            var data = _IPcpndprocesService.SearchPatient();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiologist Doctor List", data);
        }




        [HttpPost("IndicationtList")]
        //[Permission]
        public async Task<IActionResult> IndicationList(GridRequestModel objGrid)
        {
            IPagedList<IndicationListDto> IndicationtList = await _IPcpndprocesService.GetList(objGrid);
            return Ok(IndicationtList.ToGridResponse(objGrid, "Indicationt  List"));
        }

        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(PcpndprocessModel obj)
        {
            TPcpndprocess model = obj.MapTo<TPcpndprocess>();
            if (obj.PcpndtprocessId == 0)
            {
                foreach (var q in model.TPcpndprocessDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;
                    q.ModifiedDate = AppTime.Now;
                    q.ModifiedBy = CurrentUserId;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPcpndprocesService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.PcpndtprocessId);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(PcpndprocessModel obj)
        {
            TPcpndprocess model = obj.MapTo<TPcpndprocess>();
            if (obj.PcpndtprocessId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TPcpndprocessDetails)
                {
                    if (q.PcpndprocessDetId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.PcpndprocessDetId = 0;
                }

              
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPcpndprocesService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.PcpndtprocessId);
        }
    }
}
