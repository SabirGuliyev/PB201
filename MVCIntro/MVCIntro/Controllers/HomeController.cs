using Microsoft.AspNetCore.Mvc;

namespace MVCIntro.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [Route("korporativ-satislar")]
        public IActionResult Corporate()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null || id <= 0)
            {
                //return Content("Id yanlishdir");

                //return RedirectToAction("Error"));
              
                return RedirectToAction(nameof(Error));
            }

            return View("Product");

            //var student = new JsonResult(new
            //{
            //    Id = 1,
            //    Name = "Huseyn",
            //    Surname = "Jafarli"
            //});
            //return student;

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
