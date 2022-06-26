using E_market.Core.Application.ViewModels.Categories;
using E_market.Core.Application.Custom_Data_Annotations;
using Microsoft.AspNetCore.Http;
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
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        [Required(ErrorMessage = "You have to set the price for the article")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required(ErrorMessage = "You have to describe the article")]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "You have to set the category for the article")]
        public int CategoryId { get; set; }
        [DataType(DataType.Upload)]
        [MaxRange4(ErrorMessage = "You can upload a maximum of 4 images")]
        public List<IFormFile> Files { get; set; }
        public int UserId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
