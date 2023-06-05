using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

public class CourseSelectionController : Controller
{
    private readonly ICourseOfferRepository courseOfferRepository;
    private readonly ICourseMaterialRepository courseMaterialRepository;
    private readonly IRegistrationRepository registrationRepository;
    private readonly UserManager<IdentityUser> userManager;

    public CourseSelectionController(
        ICourseOfferRepository courseOfferRepository,
        ICourseMaterialRepository courseMaterialRepository,
        IRegistrationRepository registrationRepository,
         UserManager<IdentityUser> userManager)
    {
        this.courseOfferRepository = courseOfferRepository;
        this.courseMaterialRepository = courseMaterialRepository;
        this.registrationRepository = registrationRepository;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        // Step 1: Load currently offered classes
        var courseOffers = await courseOfferRepository.GetAllAsync();

        if (!courseOffers.Any())
        {
            // Show error message
            return RedirectToAction("List", "MyCourse");
        }

        // Step 2: load currently offered materials to buy
        var courseMaterials = await courseMaterialRepository.GetAllAsync();

        if (courseMaterials == null)
        {
            // Show error message
            return RedirectToAction("List", "MyCourse");
        }

        // Step 3: Add offered classes and course materials into registration view model
        var model = new CourseRegistrationView();
        model.CourseSchedules =
            MapCourseOffersToCourseScheduleViewsModel(courseOffers);
        model.CourseMaterials =
            MapCourseMaterialsToViewModels(courseMaterials);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseRegistrationView model)
    {
        // Step 1: Check if there is any course selection
        //         Course materials are optional
        Guid courseOfferId = model.SelectedScheduleId;
        if (courseOfferId == Guid.Empty)
        {
            // Send error message
            return View(model);
        }

        // To set all 'Date' and 'Time' the same
        DateTime currentDateAndTime = DateTime.Now;

        // Step 2: Create a 'Registration' entity
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        Guid registrationId = Guid.NewGuid();
        var registration = new Registration()
        {
            Id = registrationId,
            StudentId = loginId,
            CourseOfferId = courseOfferId,
            Status = Registration.Unpaid,
            CreatedOn = currentDateAndTime
        };

        // Step 3: Create an order entities if there is any material selected
        Guid orderId = Guid.NewGuid();
        if (model.SelectedMaterialIds != null && model.SelectedMaterialIds.Any())
        {
            // Step 3.1: Create an order        
            var courseMaterialOrder = new CourseMaterialOrder()
            {
                Id = orderId,
                RegistrationId = registrationId,
                OrderStatus = CourseMaterialOrder.OrderUnpaid,
                OrderDate = currentDateAndTime
            };
            
            // Step 3.2: Create an order item(s)
            List <CourseMaterialOrderItem> orderItems = new();
            foreach (Guid materialId in model.SelectedMaterialIds)
            {
                var orderItem = new CourseMaterialOrderItem()
                {
                    CourseMaterialOrderId = orderId,
                    CourseMaterialId = materialId,
                    Quantity = 1
                };
                orderItems.Add(orderItem);
            }

            courseMaterialOrder.CourseMaterialOrderItems = orderItems;
            registration.CourseMaterialOrder = courseMaterialOrder;
        }

        // Step 4: Save the entities to database
        await registrationRepository.AddAsync(registration);

        return RedirectToAction("Add", "Payment", new { 
            registrationId,
            courseOfferId, 
            orderId  
        });
    }

    //
    // Mapping Methods for Course Schedule
    private IEnumerable<CourseScheduleView> MapCourseOffersToCourseScheduleViewsModel(
        IEnumerable<CourseOffer> models)
    {
        List<CourseScheduleView> courseScheduleViews = new();
        foreach (var model in models)
        {
            courseScheduleViews.Add(MapCourseOfferToCourseScheduleViewModel(model));
        }

        return courseScheduleViews;
    }

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

    //
    // Mapping Methods for Course Material
    private IEnumerable<CourseMaterialView> MapCourseMaterialsToViewModels(
        IEnumerable<CourseMaterial> models)
    {
        List<CourseMaterialView> courseMaterialViews = new();

        foreach (var model in models)
        {
            courseMaterialViews.Add(MapCourseMaterialToViewModel(model));
        }

        return courseMaterialViews;
    }

    private CourseMaterialView MapCourseMaterialToViewModel(CourseMaterial model)
    {
        return new CourseMaterialView
        {
            Id = model.Id,
            Name = model.Name,
            Category = model.Category,
            Description = model.Description,
            Price = Convert.ToDecimal(model.Price.ToString("0.####"))
        };
    }
}
