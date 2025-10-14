using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Marketing
{
    public class Market_HospitalMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
