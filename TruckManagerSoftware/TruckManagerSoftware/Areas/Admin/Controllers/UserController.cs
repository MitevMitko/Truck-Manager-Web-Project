namespace TruckManagerSoftware.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Data.Models;

    [Area("Admin")]
    [Authorize]
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
