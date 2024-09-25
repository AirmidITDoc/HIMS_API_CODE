using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    public class BillingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
