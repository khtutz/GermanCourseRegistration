using AutoMapper;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseMaterialController : Controller
{
    private readonly IAdminCourseMaterialService adminCourseMaterialService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IMapper mapper;

    private const string AddAction = "Add";
    private const string EditAction = "Edit";

    public AdminCourseMaterialController(
        IAdminCourseMaterialService adminCourseMaterialService,
        UserManager<IdentityUser> userManager,
        IMapper mapper)
    {
        this.adminCourseMaterialService = adminCourseMaterialService;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var courseMaterialResults = await adminCourseMaterialService.GetAllAsync();

        var viewModels = mapper.Map<List<CourseMaterialView>>(courseMaterialResults);

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var viewModel = new CourseMaterialView();

        // Load the categories to select
        var categories = adminCourseMaterialService.GetCourseMaterialCategories();
        viewModel.AvailableCategories = categories.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseMaterialView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        bool isAdded = await adminCourseMaterialService.AddAsync(
            viewModel.Name,
            viewModel.Description,
            viewModel.Category,
            viewModel.Price,
            loginId,
            DateTime.Now);

        if (isAdded)
        {
            TempData["SuccessMessage"] = "Course material added successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add course material.";
        }

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var courseMaterialResult = await adminCourseMaterialService.GetByIdAsync(id);

        var viewModel = mapper.Map<CourseMaterialView>(courseMaterialResult);

        // Load the categories to select
        var categories = adminCourseMaterialService.GetCourseMaterialCategories();
        viewModel.AvailableCategories = categories.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseMaterialView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        CourseMaterialResult courseMaterialResult = 
            await adminCourseMaterialService.UpdateAsync(
                viewModel.Id,
                viewModel.Name,
                viewModel.Description,
                viewModel.Category,
                viewModel.Price,
                loginId,
                DateTime.Now);

        if (courseMaterialResult.CourseMaterial != null) 
        {
            TempData["SuccessMessage"] = "Course material updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update course material.";
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        CourseMaterialResult courseMaterialResult = 
            await adminCourseMaterialService.DeleteAsync(id);

        if (courseMaterialResult.CourseMaterial != null)
        {
            TempData["SuccessMessage"] = "Course material deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete course material.";
        }

        return RedirectToAction("List");
    }
}
