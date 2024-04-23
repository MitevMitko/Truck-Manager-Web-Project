namespace TruckManagerSoftware.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.User;
    using Core.Services.Contract;
    using Infrastructure.Data.Models;

    using static Common.DataConstants.DataConstants.Unauthorized;
    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;

    [Area(UserAreaName)]
    [Authorize(Roles = UserRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly IUserService userService;

        public UserController(UserManager<User> userManager,
            IUserService userService
            )
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await userService.LogoutUser();

                return RedirectToAction("Index", "Home", new { area = UnauthorizedAreaName });
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                UserInfoViewModel serviceModel = await userService.GetUserInfoById(id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            try
            {
                UserInfoViewModel userInfo = await userService.GetUserInfoById(id);

                return View(new ChangeStatusViewModel()
                {
                    Id = userInfo.Id,
                    Status = userInfo.Status
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusViewModel model)
        {
            // If the entered data
            // Is not valid
            // Return the model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await userService.ChangeUserStatus(model);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult UploadImage()
        {
            try
            {
                // Get user with
                // UserName == User.Identity.Name
                // From the database
                var user = userManager.FindByNameAsync(User.Identity!.Name);

                return View(new UploadUserImageViewModel()
                {
                    Id = user.Result.Id
                });
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(UploadUserImageViewModel model)
        {
            try
            {
                await userService.UploadUserImage(model);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}
