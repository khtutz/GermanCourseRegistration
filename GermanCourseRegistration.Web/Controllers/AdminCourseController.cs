using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseController : Controller
{
    private readonly ICourseRepository courseRepository;
    private readonly UserManager<IdentityUser> userManager;

    private const string AddAction = "Add";
    private const string EditAction = "Edit";

    public AdminCourseController(
        ICourseRepository courseRepository,
        UserManager<IdentityUser> userManager)
    {
        this.courseRepository = courseRepository;
        this.userManager = userManager;
    }

    //
    // Reading Method
    [HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<Course> courses = Enumerable.Empty<Course>();

        try
        {
            courses = await courseRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        var models = new List<CourseView>();

        foreach (var course in courses)
        {
            models.Add(MapCourseToViewModel(course));
        }

        return View(models);
    }

    //
    // Writing Methods
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseView model)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);
        var course = MapViewModelToCourse(model, loginId, AddAction);

        bool isAdded = false;

        try
        {
            isAdded = await courseRepository.AddAsync(course);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (isAdded)
        {
            // Show success notification
            return RedirectToAction("List");
        }

        // Show error notification
        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Course? course = null;

        try
        {
            course = await courseRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (course == null) return View("Error");

        var model = MapCourseToViewModel(course);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseView model)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);
        var course = MapViewModelToCourse(model, loginId, EditAction);

        Course? updatedCourse = null;

        try
        {
            updatedCourse = await courseRepository.UpdateAsync(course);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (updatedCourse != null)
        {
            // Show success notification
            return RedirectToAction("List");
        }

        // Show error notification
        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        Course? deletedCourse = null;

        try
        {
            deletedCourse = await courseRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }    

        if (deletedCourse != null)
        {
            // Show success notification
            return RedirectToAction("List");
        }

        // Show error notification
        return RedirectToAction("List", new { id });
    }

    //
    // Private Methods
    private Course MapViewModelToCourse(CourseView model, Guid loginId, string action)
    {
        if (action == AddAction)
        {
            return new Course
            {
                Level = model.Level,
                Part = model.Part,
                Description = model.Description,
                CreatedBy = loginId,
                CreatedOn = DateTime.Now
            };
        }
        else if (action == EditAction)
        {
            return new Course
            {
                Id = model.Id,
                Level = model.Level,
                Part = model.Part,
                Description = model.Description,
                LastModifiedBy = loginId,
                LastModifiedOn = DateTime.Now
            };
        }
        else
        {
            return new();
        }
    }

    private CourseView MapCourseToViewModel(Course model)
    {
        return new CourseView
        {
            Id = model.Id,
            Level = model.Level,
            Part = model.Part,
            Description = model.Description
        };
    }
}
