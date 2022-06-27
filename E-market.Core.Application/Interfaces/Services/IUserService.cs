using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Application.ViewModels.Users;

namespace E_market.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginUserViewModel loginVm);
        Task<bool> FindUserNameAvailabilty(string userName);
    }
}
