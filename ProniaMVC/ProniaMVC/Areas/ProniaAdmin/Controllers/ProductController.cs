﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVC.Areas.ProniaAdmin.ViewModels;

using ProniaMVC.DAL;
using ProniaMVC.Models;

namespace ProniaMVC.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetAdminProductVM> productVMs = await _context.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary == true))
                .Select(p => new GetAdminProductVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    Image = p.ProductImages.FirstOrDefault().ImageURL
                })
                .ToListAsync();

            return View(productVMs);
        }

        public async Task<IActionResult> Create()
        {


            CreateProductVM productVM = new CreateProductVM
            {
                Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync()
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            productVM.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {

                return View(productVM);
            }

            bool result = await _context.Categories.AnyAsync(c => c.Id == productVM.CategoryId && c.IsDeleted == false);
            if (!result)
            {
                ModelState.AddModelError("CategoryId", "Category does not exist");


                return View(productVM);
            }
            Product product = new Product
            {
                CategoryId = productVM.CategoryId.Value,
                SKU = productVM.SKU,
                Description = productVM.Description,
                Name = productVM.Name,
                Price = productVM.Price,
                CreatedAt = DateTime.Now,
                IsDeleted = false

            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            if (product == null) return NotFound();

            UpdateProductVM productVM = new UpdateProductVM
            {
                Name=product.Name,
                Description=product.Description,
                Price=product.Price,
                SKU=product.SKU,
                CategoryId=product.CategoryId,
                Categories= await _context.Categories.Where(c => !c.IsDeleted).ToListAsync()
            };

            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateProductVM productVM)
        {
            if (id == null || id < 1) return BadRequest();
            Product existed = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            if (existed == null) return NotFound();


           productVM.Categories= await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            if (existed.CategoryId != productVM.CategoryId)
            {
                bool result = await _context.Categories.AnyAsync(c => c.Id == productVM.CategoryId && c.IsDeleted == false);
                if (!result)
                {
                    ModelState.AddModelError("CategoryId", "Category does not exist");
                    return View(productVM);
                }
            }
            

            return View(productVM);
        }
    }
}
