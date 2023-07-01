using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;
using Microsoft.AspNetCore.Identity;

namespace GermanCourseRegistration.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly UserManager<IdentityUser> userManager;

    public UserService(
        IUserRepository userRepository,
         UserManager<IdentityUser> userManager)
    {
        this.userRepository = userRepository;
        this.userManager = userManager;
    }

    public async Task<IEnumerable<UserResult>> GetAllAsync()
    {
        IEnumerable<IdentityUser?> identityUsers = await userRepository.GetAllAsync();

        IEnumerable<UserResult> userResults = identityUsers.Select(u => new UserResult(u));

        return userResults;
    }

    public async Task<bool> AddAsync(string userName, string email, string password, bool adminRoleChecked)
    {
        var identityUser = new IdentityUser()
        {
            UserName = userName,
            Email = email
        };

        var identityResult = await userManager.CreateAsync(
            identityUser, password);

        if (identityResult.Succeeded)
        {
            var roles = new List<string> { "Users" };

            if (adminRoleChecked)
            {
                roles.Add("Admin");
            }

            identityResult = await userManager.AddToRolesAsync(identityUser, roles);

            if (identityResult != null && identityResult.Succeeded)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user != null)
        {
            var identityResult = await userManager.DeleteAsync(user);

            if (identityResult != null && identityResult.Succeeded)
            {
                return true;
            }
        }

        return false;
    }
}
