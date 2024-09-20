using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    public class OPCasepaperServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
