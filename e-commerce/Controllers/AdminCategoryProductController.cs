﻿using e_commerce.Services;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using e_commerce.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using e_commerce.Data;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //     [Route("admin/CreateCategory")]
        //   public async Task<IActionResult> Create(Category category)

        //  {
        //      if (ModelState.IsValid)
        //    {
        //       var cats = await _categoryService.CreateAsync(category);
        //      return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        // }

        //update category
        [HttpGet]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            Category categoryFromDB = await _categoryService.GetByIdAsync(id.Value);
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost]
        [Route("UpdateCategory")]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //delete category
        [HttpPost]
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            Category categoryFromDB = await _categoryService.GetByIdAsync(id.Value);
            if (categoryFromDB == null)
            {
                return NotFound(nameof(Category));
            }
            await _categoryService.DeleteAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeletePOST(int? id)

        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            Category? categoryFromDB = await _categoryService.GetByIdAsync(id.Value);
            if (categoryFromDB == null)
            {
                return NotFound(nameof(Category));
            }

            await _categoryService.DeleteAsync(id.Value);
            return RedirectToAction(nameof(Index));

        }
       







    }
}

