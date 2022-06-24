using E_market.Core.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.ViewModels.Articles
{
    public class SaveArticleViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "This article needs a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You have to include at least one image")]
        public string ImgUrl { get; set; }
        [Required(ErrorMessage = "You have to set the price for the article")]
        public double Price { get; set; }
        [Required(ErrorMessage = "You have to describe the article")]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "You have to set the category for the article")]
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
