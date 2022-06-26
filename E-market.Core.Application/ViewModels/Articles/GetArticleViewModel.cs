using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.ViewModels.Articles
{
    public class GetArticleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string UserName { get; set; }
        public List<IFormFile> Files { get; set; }

        public int CategoryId { get; set; }
    }
}
