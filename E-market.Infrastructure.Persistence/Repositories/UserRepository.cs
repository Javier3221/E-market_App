using E_market.Infrastructure.Persistence.Contexts;
using E_market.Core.Application.Interfaces.Repositories;
using E_market.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Domain.Entities;
using E_market.Core.Application.ViewModels.Users;
using Microsoft.EntityFrameworkCore;

namespace E_market.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AddAsync(User entity)
        {
            entity.Password = PasswordEcryption.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
        }
    }
}
