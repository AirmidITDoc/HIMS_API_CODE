using Asp.Versioning;
using HIMS.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabSampleController : BaseController
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
