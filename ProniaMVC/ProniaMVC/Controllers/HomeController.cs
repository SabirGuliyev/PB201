using Microsoft.AspNetCore.Mvc;
using ProniaMVC.Models;
using ProniaMVC.ViewModels;

namespace ProniaMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            List<Slide> slides= new List<Slide> { 
            new Slide{
            Id = 1,
            Title="Title 1",
            SubTitle="SubTitle 1",
            Description="Endirim var",
            Order=1,
            Image="1-10-270x300.jpg"

            },
             new Slide{
            Id = 2,
            Title="Title 2",
            SubTitle="SubTitle 2",
            Description="Endirim var helede",
            Order=3,
            Image="1-2.png"

            },
              new Slide{
            Id = 3,
            Title="Title 3",
            SubTitle="SubTitle 3",
            Description="Endirim var sabaha qeder",
            Order=2,
            Image="1-3.png"

            }

            };

            HomeVM homeVM = new HomeVM { 
            Slides = slides.OrderBy(s=>s.Order).Take(2).ToList()
            };
            return View(homeVM);
        }
    }
}
