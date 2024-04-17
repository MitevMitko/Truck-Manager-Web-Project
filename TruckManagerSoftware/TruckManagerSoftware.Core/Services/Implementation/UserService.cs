namespace TruckManagerSoftware.Core.Services.Implementation
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using SixLabors.ImageSharp;

    using System;
    using System.Threading.Tasks;

    using Contract;
    using Infrastructure.Data.Models;
    using Infrastructure.UnitOfWork.Contract;
    using Models.Order;
    using Models.Garage;
    using Models.Truck;
    using Models.User;

    using static Common.DataConstants.DataConstants.Image;
    using static Common.DataConstants.DataConstants.User;
    using static Common.Messages.Messages.Common;
    using static Common.Messages.Messages.Image;
    using static Common.Messages.Messages.User;

    public class UserService : IUserService
    {
        private readonly SignInManager<User> signInManager;

        private readonly UserManager<User> userManager;

        private readonly IImageService imageService;

        private readonly IUnitOfWork unitOfWork;

        private readonly IWebHostEnvironment webHostEnvironment;

        public UserService(SignInManager<User> signInManager,
            UserManager<User> userManager,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.imageService = imageService;
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task ChangeUserStatus(ChangeStatusViewModel model)
        {
            // Get the user
            // With Id == model.Id
            // From the database
            User user = await userManager.FindByIdAsync(model.Id.ToString());

            // If the user does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Assign the data
            // From the model
            // To the user.Status
            user.Status = model.Status;

            // Save changes to the database
            await unitOfWork.CompleteAsync();
        }

        public async Task<UserInfoViewModel> GetUserInfoById(Guid id)
        {
            // Get user with
            // Id == id
            // From the database
            User user = await userManager.FindByIdAsync(id.ToString());

            // If the user does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Create UserInfoViewModel
            // Assign the data from the user
            UserInfoViewModel userInfo = new UserInfoViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status,
                Avatar = user.Avatar
            };

            // If the user's property called GarageId has value
            // Get the user's garage from the database
            // If the user's property GarageId does not have value
            // Return null
            if (user.GarageId.HasValue)
            {
                // Get the user's garage
                // With Id == user.GarageId
                // From the database
                Garage userGarage = await unitOfWork.Garage.GetById(user.GarageId.Value);

                // Assign the data from userGarage
                // To GarageInfoViewModel
                userInfo.GarageInfo = new GarageInfoViewModel()
                {
                    Id = userGarage.Id,
                    Country = userGarage.Country,
                    City = userGarage.City,
                    Size = userGarage.Size
                };
            }
            else
            {
                userInfo.GarageInfo = null;
            }

            // If the user's property called TruckId has value
            // Get the user's truck from the database
            // If the user's property TruckId does not have value
            // Return null
            if (user.TruckId.HasValue)
            {
                // Get the user's truck
                // With Id == user.TruckId
                // From the database
                Truck userTruck = await unitOfWork.Truck.GetById(user.TruckId.Value);

                // Assign the data from userTruck
                // To TruckInfoViewModel
                userInfo.TruckInfo = new TruckInfoViewModel()
                {
                    Id = userTruck.Id,
                    Brand = userTruck.Brand,
                    Series = userTruck.Series
                };
            }
            else
            {
                userInfo.TruckInfo = null;
            }

            // If the user's property called OrderId has value
            // Get the user's order from the database
            // If the user's property OrderId does not have value
            // Return null
            if (user.OrderId.HasValue)
            {
                // Get the user's order
                // With Id == user.OrderId
                // From the database
                Order userOrder = await unitOfWork.Order.GetById(user.OrderId.Value);

                // Assign the data from userOrder
                // To OrderInfoViewModel
                userInfo.OrderInfo = new OrderInfoViewModel()
                {
                    Id = userOrder.Id,
                    Cargo = userOrder.Cargo
                };
            }
            else
            {
                userInfo.OrderInfo = null;
            }

            return userInfo;
        }

        public async Task<IList<string>> LoginUser(LoginViewModel model)
        {
            // Get user with
            // UserName == model.UserName
            // From the database
            User user = await userManager.FindByNameAsync(model.UserName);

            // If the user does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Try to sign in the user
            SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            // Initialize variable
            // For user's roles
            IList<string> userRoles = new List<string>();

            // If the user is successfully signed in
            if (result.Succeeded)
            {
                // Get the user's roles
                IList<string> roles = await userManager.GetRolesAsync(user);

                userRoles = roles;
            }
            else
            {
                throw new ArgumentException(SomethingWentWrongMessage);
            }

            return userRoles;
        }

        public async Task LogoutUser()
        {
            // Sign out the user
            await signInManager.SignOutAsync();
        }

        public async Task RegisterUser(RegisterViewModel model)
        {
            // Get user with
            // UserName == model.UserName
            // From the database
            User user =  await userManager.FindByNameAsync(model.UserName);

            // If the user exists
            // Throw argument exception
            if (user != null)
            {
                throw new ArgumentException(UserUserNameAlreadyExits);
            }

            // Get user with
            // Email == model.Email
            // From the database
            user = await userManager.FindByEmailAsync(model.Email);

            // If the user exists
            // Throw argument exception
            if (user != null)
            {
                throw new ArgumentException(UserEmailAlreadyExists);
            }

            // Create new user
            user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Status = StatusValueWhenRegisterUser
            };

            // Try to create new user
            // Using the userManager
            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            // If the user is created successfully
            // Add the user to role called User
            // And then redirect to action Login
            // In User Controller
            // On other hand throw argument exception
            if (result.Succeeded)
            {
                // Assign the user
                // To role User
                await userManager.AddToRoleAsync(user, UserRoleName);
            }
            else
            {
                throw new ArgumentException(SomethingWentWrongMessage);
            }
        }

        public async Task UploadUserImage(UploadUserImageViewModel model)
        {
            // Get the user with
            // Id == model.Id
            // From the database
            User user = await userManager.FindByIdAsync(model.Id.ToString());

            // If the user does not exist
            // Throw argument exception
            if (user == null)
            {
                throw new ArgumentException(UserNotExistMessage);
            }

            // Check if there is
            // uploaded image file
            if (model.Image != null && model.Image.Length != 0)
            {
                // Get the physical path
                // To the root directory
                // Of the web application
                string rootPath = webHostEnvironment.ContentRootPath;

                // Combine the root path
                // With the avatar's images folder
                string avatarsImagesPath = Path.Combine(rootPath, UsersAvatarsPath);

                // Check if the avatar's images path exists
                // If the path does not exist
                // Throw argument exception
                if (!Directory.Exists(avatarsImagesPath))
                {
                    throw new ArgumentException(FolderNotExistMessage);
                }

                if (user.Avatar != null && user.Avatar.Length != 0)
                {
                    // Create the destination path with
                    // The user's avatar title
                    string imagePath = Path.Combine(avatarsImagesPath, $"{user.Avatar}.jpg");

                    // Check if the image
                    // From the image path exists
                    if (!File.Exists(imagePath))
                    {
                        throw new ArgumentException(ImageNotExistMessage);
                    }

                    imageService.RemoveImage(imagePath);
                }

                // Convert the uploaded file
                // From IFormFile to Image
                Image convertedUploadedFile = imageService.ConvertIFormFileToImage(model.Image);

                // Check if the uploaded
                // File should be resized
                if (imageService.CheckIfImageShouldBeResized(convertedUploadedFile, ImageWidth, ImageHeight))
                {
                    // Returns the resized uploaded file
                    Image resizedImage = imageService.ResizeImage(convertedUploadedFile, ImageWidth, ImageHeight);

                    // Saves the resized uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(resizedImage, avatarsImagesPath);

                    user.Avatar = imageTitle;
                }
                else
                {
                    // Saves the uploaded file
                    // And returns the new title
                    // Of the uploaded file
                    string imageTitle = imageService.SaveImage(convertedUploadedFile, avatarsImagesPath);

                    user.Avatar = imageTitle;
                }

                // Save changes to the database
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<ICollection<UserInfoViewModel>> GetAllUsersInfo()
        {
            // Create collection of UserInfoViewModel
            ICollection<UserInfoViewModel> model = new List<UserInfoViewModel>();

            // Get all users
            // From the database
            List<User> users = userManager.Users.ToList();

            foreach (User user in users)
            {
                // Assign the data from the user
                // To UserInfoViewModel
                UserInfoViewModel userInfo = new UserInfoViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Status = user.Status,
                    Avatar = user.Avatar
                };

                // If the user's property called GarageId has value
                // Get the user's garage from the database
                // If the user's property GarageId does not have value
                // Return null
                if (user.GarageId.HasValue)
                {
                    // Get the user's garage
                    // With Id == user.GarageId
                    // From the database
                    Garage userGarage = await unitOfWork.Garage.GetById(user.GarageId.Value);

                    // Assign the data from userGarage
                    // To GarageInfoViewModel
                    userInfo.GarageInfo = new GarageInfoViewModel()
                    {
                        Id = userGarage.Id,
                        Country = userGarage.Country,
                        City = userGarage.City,
                        Size = userGarage.Size
                    };
                }
                else
                {
                    userInfo.GarageInfo = null;
                }

                // If the user's property called TruckId has value
                // Get the user's truck from the database
                // If the user's property TruckId does not have value
                // Return null
                if (user.TruckId.HasValue)
                {
                    // Get the user's truck
                    // With Id == user.TruckId
                    // From the database
                    Truck userTruck = await unitOfWork.Truck.GetById(user.TruckId.Value);

                    // Assign the data from userTruck
                    // To TruckInfoViewModel
                    userInfo.TruckInfo = new TruckInfoViewModel()
                    {
                        Id = userTruck.Id,
                        Brand = userTruck.Brand,
                        Series = userTruck.Series
                    };
                }
                else
                {
                    userInfo.TruckInfo = null;
                }

                // If the user's property called OrderId has value
                // Get the user's order from the database
                // If the user's property OrderId does not have value
                // Return null
                if (user.OrderId.HasValue)
                {
                    // Get the user's order
                    // With Id == user.OrderId
                    // From the database
                    Order userOrder = await unitOfWork.Order.GetById(user.OrderId.Value);

                    // Assign the data from userOrder
                    // To OrderInfoViewModel
                    userInfo.OrderInfo = new OrderInfoViewModel()
                    {
                        Id = userOrder.Id,
                        Cargo = userOrder.Cargo
                    };
                }
                else
                {
                    userInfo.OrderInfo = null;
                }

                model.Add(userInfo);
            }

            return model;
        }
    }
}
