using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.Interfaces.Repositories;
using E_market.Core.Application.ViewModels.Categories;
using E_market.Core.Domain.Entities;

namespace E_market.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Add(CategoryViewModel vm)
        {
            Category category = new();
            category.Name = vm.Name;
            category.Description = vm.Description;

            await _categoryRepository.AddAsync(category);
        }

        public async Task Update(CategoryViewModel vm)
        {
            Category category = new();
            category.Id = vm.Id;
            category.Name = vm.Name;
            category.Description = vm.Description;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<CategoryViewModel> GetByIdSaveViewModel(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            CategoryViewModel vm = new();
            vm.Id = category.Id;
            vm.Name = category.Name;
            vm.Description = category.Description;

            return vm;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var categoryList = await _categoryRepository.GetAllWithIncludeAsync(new List<string> {"Articles"});

            return categoryList.Select(category => new CategoryViewModel
            {
                Name = category.Name,
                Id = category.Id,
                Description = category.Description,
                ArticleCount = category.Articles.Count()
            }).ToList();
        }
    }
}
