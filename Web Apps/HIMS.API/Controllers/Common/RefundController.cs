using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    public class RefundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
