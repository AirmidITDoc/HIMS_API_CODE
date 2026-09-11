using HIMS.API.Models.AbhaIntegration;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.AbhaIntegration
{
    [ApiController]
    [Route("api/[controller]")]
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
