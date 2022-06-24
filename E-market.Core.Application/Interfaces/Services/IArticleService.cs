using E_market.Core.Application.ViewModels.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.Interfaces.Services
{
    public interface IArticleService : IGenericService<SaveArticleViewModel, GetArticleViewModel>
    {
        Task<List<GetArticleViewModel>> GetAllViewModelFiltered(FilterArticleViewModel filters);
    }
}
