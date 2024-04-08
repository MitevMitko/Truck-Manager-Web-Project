namespace TruckManagerSoftware.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static Common.DataConstants.DataConstants.Admin;
    using static Common.DataConstants.DataConstants.User;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }

        public static bool IsUser(this ClaimsPrincipal user)
        {
            return user.IsInRole(UserRoleName);
        }
    }
}
