using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Transaction;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Transaction
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class smsConfigController : BaseController
    {
        private readonly IsmsConfigService _IsmsConfigService;
        private readonly IGenericService<SmsoutGoing> _repository;
        private readonly IGenericService<TMailOutgoing> _repository1;
        private readonly IGenericService<TWhatsAppSmsOutgoing> _repository2;
        private readonly IGenericService<SmspdfConfig> _repository3;



        public smsConfigController(IsmsConfigService repository, IGenericService<SmsoutGoing> repository1, IGenericService<TMailOutgoing> repository2, IGenericService<TWhatsAppSmsOutgoing> repository3, IGenericService<SmspdfConfig> repository4)
        {
            _IsmsConfigService = repository;
            _repository = repository1;
            _repository1 = repository2;
            _repository2 = repository3;
            _repository3 = repository4;
        }

        //[HttpGet("TMailOutgoing/{id?}")]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository1.GetById(x => x.TranNo == id);     
        //    if (data == null)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "No data found.");
        //    }
        //    return data.ToSingleResponse<TMailOutgoing, TMailOutgoingModel>("TMailOutgoing");


        //}
        [HttpGet("TMailOutgoing/{id:int}")]
        public async Task<ApiResponse> Get(int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status400BadRequest,"Invalid TranNo.");
            }

            var data = await _repository1.GetAll(x => x.TranNo == id);

            if (data == null || !data.Any())
            {
                return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status404NotFound, "No data found." );
            }

            var result = data.Select(x => x.MapTo<TMailOutgoingModel>()) .ToList();

            return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status200OK,  "TMailOutgoing", result);
        }

        [HttpGet("TWhatsAppSmsOutgoing/{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Gets(int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status400BadRequest,   "Invalid TranNo." );
            }

            // Get multiple records by TranNo
            var data = await _repository2.GetAll(x => x.TranNo == id);

            if (data == null || !data.Any())
            {
                return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status404NotFound,  "No data found.");
            }

            var result = data.Select(x => x.MapTo<TWhatsAppSmsOutgoingModel>()).ToList();

            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK, "TWhatsAppSmsOutgoing", result );
        }

        //[HttpGet("TWhatsAppSmsOutgoing/{id?}")]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Gets(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository2.GetById(x => x.TranNo == id); if (data == null)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "No data found.");
        //    }
        //    return data.ToSingleResponse<TWhatsAppSmsOutgoing, TWhatsAppSmsOutgoingModel>("TWhatsAppSmsOutgoing");
        //}

        [HttpPost("SMSendoutList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SMSList(GridRequestModel objGrid)
        {
            IPagedList<SMSConfigListDto> List = await _IsmsConfigService.GetSMSconfig(objGrid);
            return Ok(List.ToGridResponse(objGrid, "SMS config List"));
        }


        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(smsConfigModel obj)
        {
            SsSmsConfig model = obj.MapTo<SsSmsConfig>();
            if (obj.Routeid == 0)
            {
                //model.AppDate = Convert.ToDateTime(obj.AppDate);

                await _IsmsConfigService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SmsConfig added successfully.", model);
        }
        [HttpPost("UPDATE")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Edit(smsConfigModel obj)
        {
            SsSmsConfig model = obj.MapTo<SsSmsConfig>();
            if (obj.Routeid == 0)
            {
                //model.AppDate = Convert.ToDateTime(obj.AppDate);

                await _IsmsConfigService.UpdateAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SmsConfig Updated successfully.", model);
        }
        [HttpPut("EmailConfiguration/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(EmailConfigurationModel obj)
        {
            EmailConfiguration model = obj.MapTo<EmailConfiguration>();
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
               
                model.IsActive = true;
                await _IsmsConfigService.UpdateAsync(model, CurrentUserId, CurrentUserName);

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Add API
        [HttpPost("SmspdfConfig")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SmspdfConfigModel obj)
        {
            SmspdfConfig model = obj.MapTo<SmspdfConfig>();
            if (obj.Smsid == 0)
            {
                //model.CreatedBy = CurrentUserId;
                //model.CreatedDate = AppTime.Now;
                await _IsmsConfigService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("SmspdfConfig")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SmspdfConfigModel obj)
        {
            SmspdfConfig model = obj.MapTo<SmspdfConfig>();
            //model.IsActive = true;
            if (obj.Smsid == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = AppTime.Now;

                await _IsmsConfigService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        [HttpPost("EmailOutgoingList")]
        [Permission(PageCode = "smsconfigrationtool", Permission = PagePermission.View)]
        public async Task<IActionResult> emailList(GridRequestModel objGrid)
        {
            IPagedList<EmailSendoutListDto> List = await _IsmsConfigService.GetEmailSconfig(objGrid);
            return Ok(List.ToGridResponse(objGrid, "Email Send List"));
        }


        [HttpPost("WhatsappSendoutList")]
        [Permission(PageCode = "smsconfigrationtool", Permission = PagePermission.View)]
        public async Task<IActionResult> WhatsappList(GridRequestModel objGrid)
        {
            IPagedList<WhatsAppsendOutListDto> List = await _IsmsConfigService.GetWhatsAppconfig(objGrid);
            return Ok(List.ToGridResponse(objGrid, "Whatsapp Send List"));
        }


       
    }
}
