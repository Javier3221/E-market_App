using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.ViewModels.Articles
{
    public class FilterArticleViewModel
    {
        public List<int>? CategoryList { get; set; }
        public string ArticleName { get; set; }
    }
}
