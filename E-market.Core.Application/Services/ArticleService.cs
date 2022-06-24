using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.Interfaces.Repositories;
using E_market.Core.Application.ViewModels.Articles;
using E_market.Core.Domain.Entities;

namespace E_market.Core.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task Add(SaveArticleViewModel vm)
        {
            Article article = new();
            article.Name = vm.Name;
            article.ImgUrl = vm.ImgUrl;
            article.Price = vm.Price;
            article.UserId = vm.UserId;
            article.Description = vm.Description;
            article.CategoryId = vm.CategoryId;

            await _articleRepository.AddAsync(article);
        }

        public async Task Update(SaveArticleViewModel vm)
        {
            Article article = new();
            article.Id = vm.Id;
            article.Name = vm.Name;
            article.ImgUrl = vm.ImgUrl;
            article.Price = vm.Price;
            article.UserId = vm.UserId;
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

        public async Task<List<GetArticleViewModel>> GetAllViewModel()
        {
            var articleList = await _articleRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            return articleList.Select(article => new GetArticleViewModel 
            {
                Name = article.Name,
                ImgUrl = article.ImgUrl,
                Id = article.Id,
                Price = article.Price,
                //UserName =
                Description = article.Description,
                Category = article.Category.Name,
                CategoryId = article.Category.Id
            }).ToList(); 
        }

        public async Task<List<GetArticleViewModel>> GetAllViewModelFiltered(FilterArticleViewModel filters)
        {
            var articleList = await _articleRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var listViewModel = articleList.Select(article => new GetArticleViewModel
            {
                Name = article.Name,
                ImgUrl = article.ImgUrl,
                Id = article.Id,
                Price = article.Price,
                //UserName =
                Description = article.Description,
                Category = article.Category.Name,
                CategoryId = article.Category.Id
            }).ToList();

            if (filters.CategoryList != null)
            {
                var filteredList = new List<GetArticleViewModel>();
                foreach (int item in filters.CategoryList)
                {
                    var list = listViewModel.Where(article => article.CategoryId == item).ToList();
                    filteredList.AddRange(list);
                }
                listViewModel = filteredList;
            }

            return listViewModel;
        }
    }
}
