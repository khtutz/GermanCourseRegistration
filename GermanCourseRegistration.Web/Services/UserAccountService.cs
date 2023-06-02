using Microsoft.AspNetCore.Identity;

namespace GermanCourseRegistration.Web.Services;

public class UserAccountService
{
    public static async Task<Guid> GetCurrentUserId(
        UserManager<IdentityUser> userManager, dynamic userObject)
    {
        Guid loginId = Guid.Empty;
        var user = await userManager.GetUserAsync(userObject);

        if (user != null)
        {
            loginId = new Guid(user.Id);
        }

        return loginId;
    }
}
