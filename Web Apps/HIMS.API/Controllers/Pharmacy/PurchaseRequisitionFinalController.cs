using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pharmacy;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PurchaseRequisitionFinalController : BaseController
    {
        private readonly IPurchaseRequisitionFinalService _IPurchaseRequisitionFinalService;
        private readonly IGenericService<TPrdetail> _repository;
        public PurchaseRequisitionFinalController(IPurchaseRequisitionFinalService repository, IGenericService<TPrdetail> repository1)
        {
            _IPurchaseRequisitionFinalService = repository;
            _repository = repository1;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PurchaseRequisitionFinalModel obj)
        {
            TPrheader model = obj.MapTo<TPrheader>();
            if (obj.Prid == 0)
            {
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPurchaseRequisitionFinalService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.Prid);
        }
    }
}
