using Microsoft.AspNetCore.Mvc;
using ViewBagExample.Models;
using ViewBagExample.ViewModels;

namespace ViewBagExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //ViewBag.Students= students;

            //ViewData["Students"]= students;

            //TempData["Name"] = "Nihat";


            List<Student> students = new()
            {
                new Student
                {
                    Id = 1,
                    Name="Ziya",
                    Age=22
                },
                 new Student
                {
                    Id = 2,
                    Name="Huseyn",
                    Age=18
                }
                 ,
                  new Student
                {
                    Id = 3,
                    Name="Ilahe",
                    Age=28
                }
                  , new Student
                {
                    Id = 4,
                    Name="Aynur",
                    Age=19
                }
            };

            List<Teacher> teachers = new()
            {
                new Teacher {Name="Sabir"},
                new Teacher {Name="Baylar"}
            };

            HomeVM homeVM= new HomeVM { 
            Students = students,
            Teachers = teachers
            };
            

            return View(homeVM);
        }

        public IActionResult About()
        {

            string text = TempData["Name"].ToString();
            TempData.Keep("Name");
         
            return Content(text);
            //return View();
        }

        public IActionResult Detail()
        {

            string text = TempData["Name"].ToString();


            return Content(text);
            //return View();
        }
    }
}
