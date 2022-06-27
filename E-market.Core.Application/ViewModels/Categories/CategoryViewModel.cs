using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "The category name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You need to describe what is this category")]
        public string Description { get; set; }
        public int ArticleCount { get; set; }
        public string UserName { get; set; }
    }
}
