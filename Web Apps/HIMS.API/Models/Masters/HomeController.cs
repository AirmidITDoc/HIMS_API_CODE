using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Models.Masters
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
