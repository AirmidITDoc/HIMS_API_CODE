using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.HomeCollection
{
    public class HomeCollectionDashboardController : Controller
    {
        private readonly IHomeCollectionService _IHomeCollectionService;

        public HomeCollectionDashboardController(IHomeCollectionService homeCollectionService)
        {
            _IHomeCollectionService = homeCollectionService;
        }

        [HttpGet("HomeCollectionDashBoard")]
        //[Permission]
        public async Task<ApiResponse> HomeCollectionDashboard(int UnitId, int PhlebotomistId, DateTime FromDate, DateTime ToDate)
        {

            var data = await _IHomeCollectionService.GetHomeCollectionDashboard(UnitId, PhlebotomistId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Home Collection DashBoard", data);
        }
    }
}
