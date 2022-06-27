using E_market.Core.Application.Helpers;
using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.ViewModels.Articles;
using E_market.Core.Application.ViewModels.Users;
using E_market_OnionMVC.Models.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

            SaveArticleViewModel articleVm = await _articleService.Add(vm);
            if(articleVm != null && articleVm.Id != 0)
            {
                articleVm.ImgUrl = UploadFile(vm.Files, articleVm.Id);
                await _articleService.Update(articleVm);
            }
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

            SaveArticleViewModel saveVm = await _articleService.GetByIdSaveViewModel(vm.Id);
            vm.ImgUrl = UploadFile(vm.Files, saveVm.Id, true, saveVm.ImgUrl);

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

            string basePath = $"/images/Articles/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory= new(path);
                foreach(FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "UserArticles", action = "ArticleList" });
        }

        private string UploadFile(List<IFormFile> files, int id, bool isEditMode = false, string imageUrls = "")
        {
            if (isEditMode && files == null)
            {
                string paths = string.Join(",", imageUrls);

                return paths;
            }
            string basePath = $"/images/Articles/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if it doesn't exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> pathList = new();
            foreach (IFormFile file in files)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new(file.FileName);
                string filename = guid + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, filename);

                using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                pathList.Add($"{basePath}/{filename}");
            }

            string pathString = string.Join(",", pathList);

            if (isEditMode)
            {
                List<string> oldPath = imageUrls.Split(',').ToList();

                foreach (string item in oldPath)
                {
                    string[] oldImagePart = item.Split("/");
                    string oldImageName = oldImagePart[^1];
                    string completeImageOldPath = Path.Combine(path, oldImageName);

                    if (System.IO.File.Exists(completeImageOldPath))
                    {
                        System.IO.File.Delete(completeImageOldPath);
                    }
                }
            }

            return pathString;
        }
    }
}
