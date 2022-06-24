﻿using Microsoft.AspNetCore.Mvc;
using E_market.Core.Application.ViewModels.Users;
using E_market.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_market_OnionMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UserViewModel userVm = await _userService.Login(loginVm);
            if (userVm != null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Incorrect Username or Password");
            }

            return View(loginVm);
        }

        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel UserVm)
        {
            if (!ModelState.IsValid)
            {
                return View(UserVm);
            }

            await _userService.Add(UserVm);
            return RedirectToRoute(new { controller = "User", action = "Login"});
        }
    }
}