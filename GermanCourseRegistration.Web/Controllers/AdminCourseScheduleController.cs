using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.HelperServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

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
        IEnumerable<CourseOfferResult> courseOfferResults =
            await adminCourseScheduleService.GetAllAsync();

        var viewModels = new List<CourseScheduleView>();

        foreach (var courseSchedule in courseOfferResults)
        {
            viewModels.Add(MapperProfiles
                .MapCourseOfferResultToCourseScheduleViewModel(courseSchedule));
        }

        return View(viewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        // Load the courses
        IEnumerable<CourseResult> courseResults = await adminCourseService.GetAllAsync();

        if (!courseResults.Any())
        {
            TempData["ErrorMessage"] = "No courses available. Please create the course first.";
            return RedirectToAction("List");
        }

        var viewModel = new CourseScheduleView();

        LoadItemsForUI(viewModel, courseResults);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseScheduleView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        bool isAdded = await adminCourseScheduleService.AddAsync(
            viewModel.Course!.Id,
            viewModel.Name,
            viewModel.ClassType,
            viewModel.Cost,
            viewModel.StartDate,
            viewModel.EndDate,
            loginId,
            DateTime.Now,
            viewModel.DaysOfWeek,
            viewModel.Timetable.StartTimeHour,
            viewModel.Timetable.StartTimeMinute,
            viewModel.Timetable.EndTimeHour,
            viewModel.Timetable.EndTimeMinute);

        if (isAdded)
        {
            TempData["SuccessMessage"] = "Course schedule added successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to add course schedule.";
        }

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {

        // Load the courses and course schedules (offered courses)
        IEnumerable<CourseResult> courseResults = 
            await adminCourseService.GetAllAsync();
        CourseOfferResult courseOfferResult = 
            await adminCourseScheduleService.GetByIdAsync(id);

        if (!courseResults.Any() || courseOfferResult.CouseOffer == null)
        {
            TempData["ErrorMessage"] = "No currently offered courses.";
            return RedirectToAction("List");
        }

        var viewModel = MapperProfiles
                .MapCourseOfferResultToCourseScheduleViewModel(courseOfferResult);

        LoadItemsForUI(viewModel, courseResults);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseScheduleView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        CourseOfferResult courseOfferResult = await adminCourseScheduleService.UpdateAsync(
            viewModel.Id,
            viewModel.Course!.Id,
            viewModel.Name,
            viewModel.ClassType,
            viewModel.Cost,
            viewModel.StartDate,
            viewModel.EndDate,
            loginId,
            DateTime.Now,
            viewModel.DaysOfWeek,
            viewModel.Timetable.StartTimeHour,
            viewModel.Timetable.StartTimeMinute,
            viewModel.Timetable.EndTimeHour,
            viewModel.Timetable.EndTimeMinute);

        if (courseOfferResult.CouseOffer != null)
        {
            TempData["SuccessMessage"] = "Course schedule updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update course schedule.";
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        CourseOfferResult? courseOfferResult = await adminCourseScheduleService.DeleteAsync(id);

        if (courseOfferResult.CouseOffer != null)
        {
            TempData["SuccessMessage"] = "Course schedule deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete course schedule.";
        }

        return RedirectToAction("List");
    }

    private void LoadItemsForUI(
        CourseScheduleView viewModel, 
        IEnumerable<CourseResult> courseResults)
    {
        // Load the drop down list items
        viewModel.AvailableCourseLevels = courseResults.Select(c => new SelectListItem
        {
            Text = c.Course!.Level.ToString() + "." + c.Course!.Part.ToString(),
            Value = c.Course!.Id.ToString()
        });

        var classTypes = adminCourseScheduleService.GetAvailableClassTypes();
        viewModel.AvailableClassTypes = classTypes.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });

        // Load the days of the week
        viewModel.DaysOfWeek = adminCourseScheduleService.GetDaysOfWeek();
    }
}