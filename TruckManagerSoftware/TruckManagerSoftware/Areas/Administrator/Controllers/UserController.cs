namespace TruckManagerSoftware.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.User;
    using Core.Services.Contract;
    using Infrastructure.Data.Models;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.DataConstants.DataConstants.Unauthorized;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.User;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly IUserService userService;

        public UserController(UserManager<User> userManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            try
            {
                // Get user with
                // UserName == User.Identity.Name
                User user = await userManager.FindByNameAsync(User.Identity!.Name);

                // If user does not exist
                // Throw argument exception
                if (user == null)
                {
                    throw new ArgumentException(UserNotExistMessage);
                }

                // Service which returns
                // Info about the user
                // With Id == user.Id
                UserInfoViewModel serviceModel = await userService.GetUserInfoById(user.Id);

                return View(serviceModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = ex.Message });
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
        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ICollection<UserInfoViewModel> serviceModel = await userService.GetAllUsersInfo();

                return Json(serviceModel);
            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest500", "Home", new { errorMessage = SomethingWentWrongMessage });
            }
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
