using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.HelperServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GermanCourseRegistration.Application.Messages.RegistrationMessages;

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
        var courseScheduleViewModels = Enumerable.Empty<CourseScheduleView>();
        CourseScheduleForStudentView? courseScheduleForStudentModel = null;

        if (User.IsInRole("Admin"))
        {
            var response = await adminCourseScheduleService.GetAllAsync();
            courseScheduleViewModels = CourseScheduleMapping.MapToViewModels(response);
        }
        else
        {
            Guid studentId = await UserAccountService.GetCurrentUserId(userManager, User);
            var studentRegistrationResponse = await registrationService.GetByStudentIdAsync(
                new GetRegistrationByStudentIdRequest(studentId));

            if (studentRegistrationResponse?.Registration?.CourseOffer != null)
            {
                courseScheduleForStudentModel = new CourseScheduleForStudentView()
                {
                    Name = studentRegistrationResponse.Registration.CourseOffer.Name,
                    ClassType = studentRegistrationResponse.Registration.CourseOffer.ClassType,
                    StartDate = studentRegistrationResponse.Registration.CourseOffer.StartDate,
                    EndDate = studentRegistrationResponse.Registration.CourseOffer.EndDate
                };
            }

        }

        var tuple = new Tuple<IEnumerable<CourseScheduleView>, CourseScheduleForStudentView>(
            courseScheduleViewModels, courseScheduleForStudentModel!);
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
