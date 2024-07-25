using e_commerce.Services;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace e_commerce.Controllers
{
    [Route("admin/[controller]")]
    public class AdminCategoryProductController : Controller
    {
        private readonly CategoryService _categoryService;
        public AdminCategoryProductController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var cats = await _categoryService.GetAllAsync();

            return View(cats);
        }

        [Route("admin/CreateCategory/[controller]")]
        public async Task<IActionResult> Create(Category category)

        {
            if (ModelState.IsValid)
            {
                var cats = await _categoryService.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }




        }
}

