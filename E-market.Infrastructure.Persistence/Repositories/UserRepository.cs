using E_market.Infrastructure.Persistence.Contexts;
using E_market.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
