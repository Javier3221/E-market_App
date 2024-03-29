﻿using E_market.Core.Application.ViewModels.Users;
using E_market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        Task<User> LoginAsync(LoginUserViewModel entity);
        Task<bool> FindUserNameAvailabilty(string userName);
    }
}
