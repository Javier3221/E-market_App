using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_market_OnionMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> CategoryList()
        {
            return View(await _categoryService.GetAllViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Add(vm);
            return RedirectToRoute(new { controller = "Categories", action = "CategoryList" });
        }

        public IActionResult Create()
        {
            return View("SaveCategory", new CategoryViewModel());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("SaveCategory", await _categoryService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Update(vm);
            return RedirectToRoute(new { controller = "Categories", action = "CategoryList" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToRoute(new { controller = "Categories", action = "CategoryList" });
        }
    }
}
