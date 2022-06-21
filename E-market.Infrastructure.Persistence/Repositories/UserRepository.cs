using E_market.Infrastructure.Persistence.Contexts;
using E_market.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Domain.Entities;

namespace E_market.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
