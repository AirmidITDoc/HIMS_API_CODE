using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    public class PurchaseOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
