using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.ViewModels.Articles;
using E_market_OnionMVC.Models;
using E_market_OnionMVC.Models.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_market_OnionMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;
        public HomeController(IArticleService articleService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(FilterArticleViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            ViewBag.Categories = await _categoryService.GetAllViewModel();
            return View(await _articleService.GetAllViewModelFiltered(vm));
        }

        public async Task<IActionResult> ArticleDetails(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            GetArticleViewModel getVm = await _articleService.GetByIdGetViewModel(id);
            getVm.FilePaths = getVm.ImgUrl.Split(',').ToList();
            getVm.ImgUrl = _articleService.GetMainImgUrl(getVm.ImgUrl);
            getVm.Category = _categoryService.GetByIdSaveViewModel(getVm.CategoryId).Result.Name;

            return View(getVm);
        }
    }
}
