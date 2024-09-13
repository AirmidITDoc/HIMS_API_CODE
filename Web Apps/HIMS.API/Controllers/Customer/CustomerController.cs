using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Customer
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
