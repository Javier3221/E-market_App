using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_market.Core.Application.Interfaces.Services;
using E_market.Core.Application.Interfaces.Repositories;
using E_market.Core.Application.ViewModels.Users;
using E_market.Core.Domain.Entities;

namespace E_market.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Add(UserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.UserName = vm.UserName;
            user.Password = vm.Password;

            await _userRepository.AddAsync(user);
        }

        public async Task Update(UserViewModel vm)
        {
            User user = new();
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.UserName = vm.UserName;
            user.Password = vm.Password;

            await _userRepository.UpdateAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<UserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            UserViewModel vm = new();
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.UserName = vm.UserName;
            user.Password = vm.Password;

            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllAsync();

            return userList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password

            }).ToList();
        }
    }
}
