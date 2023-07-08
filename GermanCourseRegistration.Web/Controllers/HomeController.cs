using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.HelperServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GermanCourseRegistration.Web.Controllers;

public class HomeController : Controller
{
    private readonly IAdminCourseScheduleService adminCourseScheduleService;
    private readonly IRegistrationService registrationService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public HomeController(
        IAdminCourseScheduleService adminCourseScheduleService,
        IRegistrationService registrationService,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        this.adminCourseScheduleService = adminCourseScheduleService;
        this.registrationService = registrationService;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
	{
        var courseScheduleModels = Enumerable.Empty<CourseScheduleView>();
        CourseScheduleForStudentView? courseScheduleForStudentModel = null;

        // For admin show the currently offered courses
        // For student, show the registered course (only one at a time)
        if (User.IsInRole("Admin"))
        {
            IEnumerable<CourseOfferResult> courseOfferResults = 
                await adminCourseScheduleService.GetAllAsync();

            courseScheduleModels = MapperProfiles
                .MapCourseOfferResultsToCourseScheduleViewModels(courseOfferResults);
        }
        else
        {
            Guid studentId = await UserAccountService.GetCurrentUserId(userManager, User);

            RegistrationResult registrationResult = 
                await registrationService.GetByStudentIdAsync(studentId);

            var registeredCourse = registrationResult.Registration;

            if (registeredCourse != null)
            {
                if (registeredCourse.CourseOffer != null)
                {
                    courseScheduleForStudentModel = new CourseScheduleForStudentView()
                    {
                        Name = registeredCourse.CourseOffer.Name,
                        ClassType = registeredCourse.CourseOffer.ClassType,
                        StartDate = registeredCourse.CourseOffer.StartDate,
                        EndDate = registeredCourse.CourseOffer.EndDate
                    };
                }
            }
        }

        var tuple = new Tuple<IEnumerable<CourseScheduleView>, CourseScheduleForStudentView>(
            courseScheduleModels, courseScheduleForStudentModel!);
        return View(tuple);
    }

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
