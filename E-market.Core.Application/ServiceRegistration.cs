using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
