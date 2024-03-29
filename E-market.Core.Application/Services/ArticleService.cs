﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.Interfaces.Repositories;
using E_market.Core.Application.ViewModels.Articles;
using E_market.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using E_market.Core.Application.ViewModels.Users;
using E_market.Core.Application.Helpers;

namespace E_market.Core.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly UserViewModel userViewModel;

        public ArticleService(IArticleRepository articleRepository, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _articleRepository = articleRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _userService = userService;
        }

        public async Task<SaveArticleViewModel> Add(SaveArticleViewModel vm)
        {
            Article article = new();
            article.Name = vm.Name;
            article.Price = vm.Price;
            article.UserId = userViewModel.Id;
            article.Description = vm.Description;
            article.CategoryId = vm.CategoryId;

            article = await _articleRepository.AddAsync(article);

            SaveArticleViewModel articleVm = new();

            articleVm.Id = article.Id;
            articleVm.Name = article.Name;
            articleVm.ImgUrl = article.ImgUrl;
            articleVm.Price = article.Price;
            articleVm.UserId = article.UserId;
            articleVm.Description = article.Description;
            articleVm.CategoryId = article.CategoryId;

            return articleVm;
        }

        public async Task Update(SaveArticleViewModel vm)
        {
            Article article = await _articleRepository.GetByIdAsync(vm.Id);
            article.Id = vm.Id;
            article.Name = vm.Name;
            article.ImgUrl = vm.ImgUrl;
            article.Price = vm.Price;
            article.UserId = userViewModel.Id;
            article.Description = vm.Description;
            article.CategoryId = vm.CategoryId;

            await _articleRepository.UpdateAsync(article);
        }

        public async Task Delete(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            await _articleRepository.DeleteAsync(article);
        }

        public async Task<SaveArticleViewModel> GetByIdSaveViewModel(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);

            SaveArticleViewModel vm = new();
            vm.Id = article.Id;
            vm.Name = article.Name;
            vm.ImgUrl = article.ImgUrl;
            vm.Price = article.Price;
            vm.UserId = article.UserId;
            vm.Description = article.Description;
            vm.CategoryId = article.CategoryId;

            return vm;
        }

        public async Task<GetArticleViewModel> GetByIdGetViewModel(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);

            GetArticleViewModel vm = new();
            vm.Id = article.Id;
            vm.Name = article.Name;
            vm.ImgUrl = article.ImgUrl;
            vm.Price = article.Price;
            vm.User = _userService.GetByIdSaveViewModel(article.UserId).Result;
            vm.Description = article.Description;
            vm.CategoryId = article.CategoryId;
            vm.DatePublished = article.Created.Date;

            return vm;
        }

        public async Task<List<GetArticleViewModel>> GetAllViewModel()
        {
            var articleList = await _articleRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return articleList.Where(article => article.UserId == userViewModel.Id).Select(article => new GetArticleViewModel
            {
                Name = article.Name,
                ImgUrl = GetMainImgUrl(article.ImgUrl),
                Id = article.Id,
                Price = article.Price,
                UserId = userViewModel.Id,
                Description = article.Description,
                Category = article.Category.Name,
                CategoryId = article.Category.Id
            }).ToList(); 
        }

        public string GetMainImgUrl(string paths)
        {
            List<string> result = paths.Split(new char[] { ',' }).ToList();
            string mainUrl = result[0];

            return mainUrl;
        }

        public async Task<List<GetArticleViewModel>> GetAllViewModelFiltered(FilterArticleViewModel filters)
        {
            var articleList = await _articleRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var listViewModel = articleList.Where(article => article.UserId != userViewModel.Id).Select(article => new GetArticleViewModel
            {
                Name = article.Name,
                ImgUrl = GetMainImgUrl(article.ImgUrl),
                Id = article.Id,
                Price = article.Price,
                User = _userService.GetByIdSaveViewModel(article.UserId).Result,
                Description = article.Description,
                Category = article.Category.Name,
                CategoryId = article.Category.Id
            }).ToList();

            if (filters.CategoryList != null && filters.ArticleName != null)
            {
                var filteredList = new List<GetArticleViewModel>();
                foreach (int item in filters.CategoryList)
                {
                    var list = listViewModel.Where(article => article.CategoryId == item && article.Name.Contains(filters.ArticleName)).ToList();
                    filteredList.AddRange(list);
                }
                listViewModel = filteredList;
            }
            else if (filters.CategoryList != null)
            {
                var filteredList = new List<GetArticleViewModel>();
                foreach (int item in filters.CategoryList)
                {
                    var list = listViewModel.Where(article => article.CategoryId == item).ToList();
                    filteredList.AddRange(list);
                }
                listViewModel = filteredList;
            }
            else if (filters.ArticleName != null)
            {
                listViewModel = listViewModel.Where(article => article.Name.Contains(filters.ArticleName)).ToList();
            }

            return listViewModel;
        }

        public async Task<List<GetArticleViewModel>> GetAllWithUser()
        {
            var articleList = await _articleRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return articleList.Select(article => new GetArticleViewModel
            {
                Name = article.Name,
                ImgUrl = GetMainImgUrl(article.ImgUrl),
                Id = article.Id,
                Price = article.Price,
                UserId = userViewModel.Id,
                Description = article.Description,
                Category = article.Category.Name,
                CategoryId = article.Category.Id,
                User = _userService.GetByIdSaveViewModel(article.UserId).Result
            }).ToList();
        }
    }
}
