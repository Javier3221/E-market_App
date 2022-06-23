using E_market.Core.Application.Interfaces.Repositories;
using E_market.Core.Domain.Entities;
using E_market.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Infrastructure.Persistence.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        private readonly ApplicationContext _dbContext;

        public ArticleRepository(ApplicationContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Article>> GetAllByCategory(int categoryId)
        {
            return await _dbContext.Set<Article>()
                .Where(article => article.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
