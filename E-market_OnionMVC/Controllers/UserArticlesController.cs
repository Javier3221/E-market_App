using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.ViewModels.Articles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_market_OnionMVC.Controllers
{
    public class UserArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public UserArticlesController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> ArticleList()
        {
            return View(await _articleService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            SaveArticleViewModel vm = new();
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveArticle", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveArticleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveArticle", vm);
            }

            vm.UserId = 1;
            await _articleService.Add(vm);
            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList" });
        }

        public async Task<IActionResult> Update(int id)
        {
            SaveArticleViewModel vm = await _articleService.GetByIdSaveViewModel(id);
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveArticle", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveArticleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveArticle", vm);
            }

            vm.UserId = 1;
            await _articleService.Update(vm);
            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList"});
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _articleService.Delete(id);
            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList" });
        }
    }
}
