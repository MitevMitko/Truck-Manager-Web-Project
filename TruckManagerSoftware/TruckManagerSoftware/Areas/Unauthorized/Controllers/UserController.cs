namespace TruckManagerSoftware.Areas.Unauthorized.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.User;
    using Core.Services.Contract;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.DataConstants.DataConstants.Unauthorized;
    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.User;

    [Area(UnauthorizedAreaName)]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // If the entered data
            // From the user is not valid
            // Return the same model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await userService.RegisterUser(model);

                TempData["Message"] = UserSuccessfullyCreated;

                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // If the entered data
            // From the user is not valid
            // Return the same model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Service which returns
                // the user's roles
                // If the user is successfully logged in
                IList<string> userRoles = await userService.LoginUser(model);

                // Redirect to action Index in Home Controller
                // If the user's roles contains AdminRoleName
                // And does not contain UserRoleName
                if (userRoles.Contains(AdminRoleName) && !userRoles.Contains(UserRoleName))
                {
                    return RedirectToAction("Index", "Home", new { area = AdminAreaName });
                }

                // Redirect to action Index in Home Controller
                // If the user's roles contains UserRoleName
                // And does not contain AdminRoleName
                return RedirectToAction("Index", "Home", new { area = UserAreaName });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}
