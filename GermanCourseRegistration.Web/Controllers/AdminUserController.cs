using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUserController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly UserManager<IdentityUser> userManager;

    public AdminUserController(
        IUserRepository userRepository,
        UserManager<IdentityUser> userManager)
    {
        this.userRepository = userRepository;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<IdentityUser> users = Enumerable.Empty<IdentityUser>();

        try
        {
            users = await userRepository.GetAll();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        // Map the domain model to the view model
        var userIndividualViews = new List<UserIndividualView>();
        foreach (var user in users)
        {
            userIndividualViews.Add(new UserIndividualView
            {
                Id = Guid.Parse(user.Id),
                Username = user.UserName!,
                EmailAddress = user.Email!
            });
        }
        var userView = new UserView { Users = userIndividualViews };

        return View(userView);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserView userView)
    {
        var identityUser = new IdentityUser
        {
            UserName = userView.Username,
            Email = userView.Email
        };

        var identityResult = await userManager.CreateAsync(
            identityUser, userView.Password);
        
        if (identityResult.Succeeded)
        {
            var roles = new List<string> { "User" };

            if (userView.AdminRoleChecked)
            {
                roles.Add("Admin");
            }

            identityResult = await userManager
                .AddToRolesAsync(identityUser, roles);

            if (identityResult != null && identityResult.Succeeded)
            {
                // Show success message
                return RedirectToAction("List", "AdminUser");
            }
        }
        else
        {
            // Read the message from identityResult and show it
            return RedirectToAction("List", "AdminUser");
        }

        // Show error message
        return RedirectToAction("List", "AdminUser");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid userId)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user != null)
        {
            var identityResult = await userManager.DeleteAsync(user);

            if (identityResult != null && identityResult.Succeeded)
            {
                return RedirectToAction("List", "AdminUser");
            }
        }

        return View();
    }
}
