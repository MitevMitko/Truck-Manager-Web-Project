﻿namespace TruckManagerSoftware.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.User;
    using Infrastructure.Data.Models;

    using static Common.DataConstants.DataConstants.User;

    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            // Check if the user is authenticated
            // If the user is authenticated
            // Redirect to action Index from Home Controller
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            // If the user is not authenticated
            // Return the register view model
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // If the entered data
            // From the user is not valid
            // Return the same model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create new user
            User user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Status = StatusValueWhenRegisterUser
            };

            // Try to create new user using
            // The user manager
            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            // If the user is created successfully
            // Redirect to action Login from User Controller
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            // If the user is not created successfully
            // Return the view
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Check if the user is authenticated
            // If the user is authenticated
            // Redirect to action Index from Home Controller
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            // If the user is not authenticated
            // Return the login view model
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // If the entered data
            // From the user is not valid
            // Return the same model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Find the user by username
            User user = await userManager.FindByNameAsync(model.UserName);

            // Check if user exists
            if (user != null)
            {
                // Returns result if the user is
                // Signed in successfully
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                // If the user is signed in successfully
                // Redirect to action Index from Home Controller
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // If the user is not signed in successfully
            // Return the view
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await signInManager.SignOutAsync();

            // Redirect to action Index from Home controller
            return RedirectToAction("Index", "Home");
        }
    }
}
