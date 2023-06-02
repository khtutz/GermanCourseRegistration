using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.Web.Models;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GermanCourseRegistration.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ICourseOfferRepository courseOfferRepository;
        private readonly IRegistrationRepository registrationRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(
			ILogger<HomeController> logger,
            ICourseOfferRepository courseOfferRepository,
            IRegistrationRepository registrationRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            this.courseOfferRepository = courseOfferRepository;
            this.registrationRepository = registrationRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
		{
            if (!signInManager.IsSignedIn(User)) return View();

            var courseScheduleModels = new List<CourseScheduleView>();
            CourseScheduleForStudentView? courseScheduleForStudentModel = null;

            // For admin show the currently offered courses
            // For student, show the registered course (only one at a time)
            if (User.IsInRole("Admin"))
            {
                var courseOffers = await courseOfferRepository.GetAllAsync();

                foreach (var courseSchedule in courseOffers)
                {
                    courseScheduleModels.Add(
                        MapCourseOfferToCourseScheduleViewModel(courseSchedule));
                }
            }
            else
            {
                Guid studentId = await UserAccountService.GetCurrentUserId(userManager, User);
                var registeredCourse = await registrationRepository.GetByStudentIdAsync(studentId);
                
                if (registeredCourse != null)
                {
                    if (registeredCourse.CourseOffer != null)
                    {
                        courseScheduleForStudentModel = MapCourseOfferToStudentScheduleView(
                            registeredCourse.CourseOffer);
                    }
                }
            }

            var tuple = new Tuple<List<CourseScheduleView>, CourseScheduleForStudentView>(
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

        //
        // Private Methods
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

        private CourseScheduleForStudentView MapCourseOfferToStudentScheduleView(
            CourseOffer model)
        {
            return new CourseScheduleForStudentView
            {
                Name = model.Name,
                ClassType = model.ClassType,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
        }
    }
}