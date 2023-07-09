using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.HelperServices;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using GermanCourseRegistration.Application.Messages.CourseOfferMessages;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseScheduleController : Controller
{
    private readonly IAdminCourseService adminCourseService;
    private readonly IAdminCourseScheduleService adminCourseScheduleService;
    private readonly UserManager<IdentityUser> userManager;

    public AdminCourseScheduleController(
        IAdminCourseService adminCourseService,
        IAdminCourseScheduleService adminCourseScheduleService,
        UserManager<IdentityUser> userManager)
    {
        this.adminCourseService = adminCourseService;
        this.adminCourseScheduleService = adminCourseScheduleService;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var response = await adminCourseScheduleService.GetAllAsync();

        var viewModels = CourseScheduleMapping.MapToViewModels(response);

        return View(viewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var response = await adminCourseService.GetAllAsync();

        if (response == null || response.Courses == null || !response.Courses.Any())
        {
            TempData[Notification.ModalMessage[0]] = 
                "No courses available. Please create the course first.";
            return RedirectToAction("List");
        }

        var courseScheduleView = new CourseScheduleView();
        var courseViews = CourseMapping.MapToViewModels(response);

        LoadItemsForUI(courseScheduleView, courseViews);

        return View(courseScheduleView);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseScheduleView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var request = CourseScheduleMapping.MapToAddRequest(viewModel, loginId, DateTime.Now);

        var response = await adminCourseScheduleService.AddAsync(request);

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var coursesResponse = await adminCourseService.GetAllAsync();
        var courseScheduleResponse = await adminCourseScheduleService.GetByIdAsync(
            new GetCourseOfferByIdRequest(id));

        if (coursesResponse == null || 
            coursesResponse.Courses == null || 
            !coursesResponse.Courses.Any() ||
            courseScheduleResponse == null ||
            courseScheduleResponse.CourseOffer == null)
        {
            TempData[Notification.ModalMessage[0]] = "No currently offered courses.";
            return RedirectToAction("List");
        }

        var courseScheduleView = CourseScheduleMapping.MapToViewModel(courseScheduleResponse);
        var courseViews = CourseMapping.MapToViewModels(coursesResponse);

        LoadItemsForUI(courseScheduleView, courseViews);

        return View(courseScheduleView);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseScheduleView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var request = CourseScheduleMapping.MapToUpdateRequest(viewModel, loginId, DateTime.Now);

        var response = await adminCourseScheduleService.UpdateAsync(request);

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await adminCourseScheduleService.DeleteAsync(
            new DeleteCourseOfferRequest(id));

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    private void LoadItemsForUI(
        CourseScheduleView courseScheduleView, 
        IEnumerable<CourseView> courseViews)
    {
        // Load the drop down list items
        courseScheduleView.AvailableCourseLevels = courseViews.Select(c => new SelectListItem
        {
            Text = c.Level.ToString() + "." + c.Part.ToString(),
            Value = c.Id.ToString()
        });

        var classTypes = adminCourseScheduleService.GetAvailableClassTypes();
        courseScheduleView.AvailableClassTypes = classTypes.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });

        // Load the days of the week
        courseScheduleView.DaysOfWeek = adminCourseScheduleService.GetDaysOfWeek();
    }
}