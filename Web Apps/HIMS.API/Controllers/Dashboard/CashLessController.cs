using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DashBoard;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Dashboard;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Dashboard
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CashLessController : BaseController
    {

        private readonly ICashLessService _ICashLessService;
        public CashLessController(ICashLessService repository)
        {
            _ICashLessService = repository;
        }
      
        [HttpPost("CashlessPatientWiseSummaryList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CashlessPatientWiseSummaryDto> CashlessPatientWiseSummaryList = await _ICashLessService.GetListAsync(objGrid);
            return Ok(CashlessPatientWiseSummaryList.ToGridResponse(objGrid, "Cashless PatientWiseSummary List"));
        }
        [HttpPost("CashlessCountSummaryList")]
        //[Permission]
        public async Task<IActionResult> CassLessCountList(GridRequestModel objGrid)
        {
            IPagedList<CashlessCountSummaryDto> CashlessPatientWiseSummaryList = await _ICashLessService.CashLessGetListAsync(objGrid);
            return Ok(CashlessPatientWiseSummaryList.ToGridResponse(objGrid, "Cashless CountSummary List"));
        }
        [HttpPost("CashlessCompanyWiseSummaryList")]
        //[Permission]
        public async Task<IActionResult> CassLessCompanyWiseCountList(GridRequestModel objGrid)
        {
            IPagedList<CashlessCompanyWiseSummaryDto> CashlessPatientWiseSummaryList = await _ICashLessService.CashLessCompanyWiseGetListAsync(objGrid);
            return Ok(CashlessPatientWiseSummaryList.ToGridResponse(objGrid, "Cashless CompanyWiseSummary List"));
        }
    }
}
