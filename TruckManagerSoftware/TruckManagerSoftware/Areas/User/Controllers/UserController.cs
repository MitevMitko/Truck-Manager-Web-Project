namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Data.Models;

    using static Common.DataConstants.DataConstants.User;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;

        public UserController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await signInManager.SignOutAsync();

            // Redirect to action Index from Home controller
            return RedirectToAction("Index", "Home", new { area = "Unauthorized" });
        }
    }
}
