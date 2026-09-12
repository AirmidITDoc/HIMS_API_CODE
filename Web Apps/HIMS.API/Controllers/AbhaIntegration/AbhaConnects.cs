using Asp.Versioning;
using HIMS.API.Models.AbhaIntegration;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.AbhaIntegration
{
    //[ApiController]
   // [Route("api/[controller]")]

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AbhaConnects : ControllerBase
    {
        private readonly IAbhaConnectService _abhaConnectService;
        public AbhaConnects(IAbhaConnectService abhaConnectService)
        {
            _abhaConnectService = abhaConnectService;
        }

        [HttpPost("InitiateClient")]
        public async Task<IActionResult> InitiateClient(InitiateClientModel model)
        {
            var result = await _abhaConnectService.InitiateClient(model);
            return Ok(result);
        }
    }
}
