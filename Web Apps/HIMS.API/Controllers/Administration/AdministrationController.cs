using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Administration;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdministrationController: BaseController
    {
        
            private readonly IAdministrationService _IAdministrationService;
            public AdministrationController(IAdministrationService repository)
            {
                _IAdministrationService = repository;
            }
        [HttpPost("PaymentModeList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PaymentModeDto> PaymentModeList = await _IAdministrationService.GetListAsync(objGrid);
            return Ok(PaymentModeList.ToGridResponse(objGrid, "PaymentMode App List"));
        }
    }
}
