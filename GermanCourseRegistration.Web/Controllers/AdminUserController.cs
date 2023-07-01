using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUserController : Controller
{
    private readonly IUserService userService;
    private readonly UserManager<IdentityUser> userManager;

    public AdminUserController(
        IUserService userService,
        UserManager<IdentityUser> userManager)
    {
        this.userService = userService;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<UserResult> users = await userService.GetAllAsync();

        var userIndividualViews = MapperProfiles.MapUserResultsToUserIndividualViews(users);

        var userView = new UserView { Users = userIndividualViews };

        return View(userView);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserView viewModel)
    {
        bool isAdded = await userService.AddAsync(
            viewModel.Username,
            viewModel.Email,
            viewModel.Password,
            viewModel.AdminRoleChecked);

        if (isAdded)
        {
            TempData["SuccessMessage"] = "User added successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add user.";
        }

        // Show error message
        return RedirectToAction("List", "AdminUser");
    }

    [HttpGet]
    public IActionResult Edit(Guid userId)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool isDeleted = await userService.DeleteAsync(id);
        var user = await userManager.FindByIdAsync(id.ToString());

        if (isDeleted)
        {
            TempData["SuccessMessage"] = "User deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete user.";
        }

        return RedirectToAction("List", "AdminUser");
    }
}
