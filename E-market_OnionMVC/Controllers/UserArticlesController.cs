using E_market.Core.Application.Helpers;
using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.ViewModels.Articles;
using E_market.Core.Application.ViewModels.Users;
using E_market_OnionMVC.Models.Middlewares;
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
        private readonly ValidateUserSession _validateUserSession;

        public UserArticlesController(IArticleService articleService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> ArticleList()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(await _articleService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveArticleViewModel vm = new();
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveArticle", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveArticleViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveArticle", vm);
            }

            await _articleService.Add(vm);
            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList" });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveArticleViewModel vm = await _articleService.GetByIdSaveViewModel(id);
            vm.Categories = await _categoryService.GetAllViewModel();
            return View("SaveArticle", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveArticleViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveArticle", vm);
            }

            await _articleService.Update(vm);
            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList"});
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _articleService.Delete(id);
            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList" });
        }
    }
}
