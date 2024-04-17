namespace TruckManagerSoftware.Core.Services.Contract
{
    using Models.User;

    public interface IUserService
    {
        Task RegisterUser(RegisterViewModel model);

        Task<IList<string>> LoginUser(LoginViewModel model);

        Task LogoutUser();

        Task<UserInfoViewModel> GetUserInfoById(Guid id);

        Task ChangeUserStatus(ChangeStatusViewModel model);

        Task UploadUserImage(UploadUserImageViewModel model);

        Task<ICollection<UserInfoViewModel>> GetAllUsersInfo();
    }
}
