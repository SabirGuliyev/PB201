using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVC.DAL;
using ProniaMVC.Models;
using ProniaMVC.Utilities.Enums;
using ProniaMVC.Utilities.Extensions;

namespace ProniaMVC.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SlideController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {

            List<Slide> slides = await _context.Slides.Where(s => s.IsDeleted == false).ToListAsync();

            return View(slides);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Test()
        {

            //56021ef9-0075-4fd0-9ff2-f48609bdf586 flower.jpg
            return Content(Guid.NewGuid().ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            //if (!ModelState.IsValid) return View();


            if (!slide.Photo.ValidateType("image/"))
            {

                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }

            if (!slide.Photo.ValidateSize(FileSize.MB,2))
            {
                ModelState.AddModelError("Photo", "File size must be less than 2mb");
                return View();
            }

           
            slide.Image =await slide.Photo.CreateFileAsync(_env.WebRootPath,"assets","images","website-images");

            slide.CreatedAt = DateTime.Now;

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
