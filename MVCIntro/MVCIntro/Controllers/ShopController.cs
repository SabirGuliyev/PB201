using Microsoft.AspNetCore.Mvc;

namespace MVCIntro.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
