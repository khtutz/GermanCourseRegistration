using AutoMapper;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseController : Controller
{
    private readonly IAdminCourseService adminCourseService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IMapper mapper;

    public AdminCourseController(
        IAdminCourseService adminCourseService,
        UserManager<IdentityUser> userManager,
        IMapper mapper)
    {
        this.adminCourseService = adminCourseService;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var courseResults = await adminCourseService.GetAllAsync();

        var viewModels = mapper.Map<List<CourseView>>(courseResults);

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        bool isAdded = await adminCourseService.AddAsync(
            viewModel.Level,
            viewModel.Part,
            viewModel.Description,
            loginId,
            DateTime.Now);

        if (isAdded)
        {
            TempData["SuccessMessage"] = "Course added successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add course.";
        }

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var courseResult = await adminCourseService.GetByIdAsync(id);

        var viewModel = mapper.Map<CourseView>(courseResult);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        CourseResult courseResult = await adminCourseService.UpdateAsync(
            viewModel.Id,
            viewModel.Level,
            viewModel.Part,
            viewModel.Description,
            loginId,
            DateTime.Now);

        if (courseResult.Course != null)
        {
            TempData["SuccessMessage"] = "Course updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update course.";
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        CourseResult courseResult = await adminCourseService.DeleteAsync(id);

        if (courseResult.Course != null)
        {
            TempData["SuccessMessage"] = "Course deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete course.";
        }

        return RedirectToAction("List");
    }
}
