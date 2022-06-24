using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E_market.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
