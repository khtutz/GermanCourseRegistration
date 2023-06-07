using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseScheduleController : Controller
{
    private readonly ICourseRepository courseRepository;
    private readonly ICourseOfferRepository courseOfferRepository;
    private readonly ITimetableRepository timetableRepository;
    private readonly UserManager<IdentityUser> userManager;

    private const string AddAction = "Add";
    private const string EditAction = "Edit";

    public AdminCourseScheduleController(
        ICourseRepository courseRepository,
        ICourseOfferRepository courseOfferRepository,
        ITimetableRepository timetableRepository,
        UserManager<IdentityUser> userManager)
    {
        this.courseRepository = courseRepository;
        this.courseOfferRepository = courseOfferRepository;
        this.timetableRepository = timetableRepository;
        this.userManager = userManager;
    }

    //
    // Reading Method
    [HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<CourseOffer> courseOffers = Enumerable.Empty<CourseOffer>();

        try
        {
            courseOffers = await courseOfferRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        var models = new List<CourseScheduleView>();

        foreach (var courseSchedule in courseOffers)
        {
            models.Add(MapCourseOfferToCourseScheduleViewModel(courseSchedule));
        }

        return View(models);
    }

    //
    // Writing Methods
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        // Load the course information
        // Courses must be created first
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

        if (!courses.Any())
        {
            // Show error message
            return RedirectToAction("List");
        }

        var model = new CourseScheduleView();

        // Load the course levels and class types
        model.AvailableCourseLevels = courses.Select(c => new SelectListItem
        {
            Text = c.Level.ToString() + "." + c.Part.ToString(),
            Value = c.Id.ToString()
        });

        var classTypes = new List<string>()
        {
            CourseOffer.OnlineClass,
            CourseOffer.InPersonClass
        };
        model.AvailableClassTypes = classTypes.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });

        // Load the days of the week
        model.DaysOfWeek = Timetable.DaysOfWeek;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseScheduleView model)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var courseOffer = MapCourseScheduleViewModelToCourseOfferDomainModel(
            model, loginId, AddAction);
        courseOffer.Timetables = MapTimetableViewModelToTimetableDomainModels(
            model.DaysOfWeek, model.Timetable);

        bool isAdded = false;

        try
        {
            isAdded = await courseOfferRepository.AddAsync(courseOffer);
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
        CourseOffer? courseOffer = null;
        IEnumerable<Course> courses = Enumerable.Empty<Course>();

        try
        {
            courseOffer = await courseOfferRepository.GetByIdAsync(id);
            courses = await courseRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (courseOffer == null || !courses.Any())
        {
            // Show error message
            return RedirectToAction("List");
        }

        var model = MapCourseOfferToCourseScheduleViewModel(courseOffer);

        // Load the course levels and class types
        model.AvailableCourseLevels = courses.Select(c => new SelectListItem
        {
            Text = c.Level.ToString() + "." + c.Part.ToString(),
            Value = c.Id.ToString()
        });

        var classTypes = new List<string>()
        {
            CourseOffer.OnlineClass,
            CourseOffer.InPersonClass
        };
        model.AvailableClassTypes = classTypes.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });

        // Load the days of the week
        model.DaysOfWeek = Timetable.DaysOfWeek;

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseScheduleView model)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var courseOffer = MapCourseScheduleViewModelToCourseOfferDomainModel(
            model, loginId, EditAction);
        courseOffer.Timetables = MapTimetableViewModelToTimetableDomainModels(
            model.DaysOfWeek, model.Timetable);

        // Delete the existing time table first
        IEnumerable<Timetable> deletedTimetables = Enumerable.Empty<Timetable>();

        try
        {
            deletedTimetables = await timetableRepository
                .DeleteByCouseOfferIdAsync(courseOffer.Id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (!deletedTimetables.Any())
        {
            // Show error notification
            return RedirectToAction("List");
        }

        // Update the course offer along with time table
        CourseOffer? updatedCourseOffer = null;

        try
        {
            updatedCourseOffer = await courseOfferRepository.UpdateAsync(courseOffer);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (updatedCourseOffer != null)
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
        CourseOffer? deletedCourseOffer = null;
        
        try
        {
            deletedCourseOffer = await courseOfferRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (deletedCourseOffer != null) 
        { 
            // Show success notification
            return RedirectToAction("List"); 
        }

        // Show error notification
        return RedirectToAction("List", new { id });
    }

    //
    // Private Methods: View Models to Domain Models
    private CourseOffer MapCourseScheduleViewModelToCourseOfferDomainModel(
        CourseScheduleView model, Guid loginId, string action)
    {
        if (action == AddAction)
        {
            return new CourseOffer
            {
                CourseId = model.Course!.Id,
                Name = model.Name,
                ClassType = model.ClassType,
                Cost = model.Cost,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedBy = loginId,
                CreatedOn = DateTime.Now
            };
        }
        else if (action == EditAction)
        {
            return new CourseOffer
            {
                Id = model.Id,
                CourseId = model.Course!.Id,
                Name = model.Name,
                ClassType = model.ClassType,
                Cost = model.Cost,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                LastModifiedBy = loginId,
                LastModifiedOn = DateTime.Now
            };
        }
        else
        {
            return new();
        }
    }

    private IEnumerable<Timetable> MapTimetableViewModelToTimetableDomainModels(
        IEnumerable<string> daysOfWeek, TimetableView model)
    {
        var timetables = new List<Timetable>();

        foreach (var day in daysOfWeek)
        {
            timetables.Add(new Timetable
            {
                DayName = day,
                StartTimeHour = model.StartTimeHour, 
                StartTimeMinute = model.StartTimeMinute,
                EndTimeHour = model.EndTimeHour,
                EndTimeMinute = model.EndTimeMinute
            });
        }

        return timetables;
    }

    //
    // Private Methods: Domain Models to View Models
    private CourseScheduleView MapCourseOfferToCourseScheduleViewModel(
        CourseOffer courseOffer)
    {
        return new CourseScheduleView
        {
            Id = courseOffer.Id,
            Name = courseOffer.Name,
            ClassType = courseOffer.ClassType,
            Cost = Convert.ToDecimal(courseOffer.Cost.ToString("0.####")),
            StartDate = courseOffer.StartDate,
            EndDate = courseOffer.EndDate,
            Course = courseOffer.Course != null ? MapCourseToViewModel(courseOffer.Course) : null, 
            Timetable = courseOffer.Timetables != null
                ? MapTimetableDomainModelsToTimetableViewModel(courseOffer.Timetables)
                : new(),
            SelectDays = courseOffer.Timetables != null
                ? courseOffer.Timetables.Select(t => t.DayName)
                : Enumerable.Empty<string>()
        };
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

    private TimetableView MapTimetableDomainModelsToTimetableViewModel(
        IEnumerable<Timetable> models)
    {
        var timetables = models.ToList();
        return new TimetableView
        {
            StartTimeHour = timetables[0].StartTimeHour,
            StartTimeMinute = timetables[0].StartTimeMinute,
            EndTimeHour = timetables[0].EndTimeHour,
            EndTimeMinute = timetables[0].EndTimeMinute
        };
    }
}