using AutoMapper;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace GermanCourseRegistration.Web.Controllers;

public class CourseSelectionController : Controller
{
    private readonly IAdminCourseScheduleService adminCourseScheduleService;
    private readonly IAdminCourseMaterialService adminCourseMaterialService;
    private readonly IRegistrationService registrationService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IMapper mapper;

    public CourseSelectionController(
         IAdminCourseScheduleService adminCourseScheduleService,
         IAdminCourseMaterialService adminCourseMaterialService,
         IRegistrationService registrationService,
         UserManager<IdentityUser> userManager,
         IMapper mapper)
    {
        this.adminCourseScheduleService = adminCourseScheduleService;
        this.adminCourseMaterialService = adminCourseMaterialService;
        this.registrationService = registrationService;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        // Step 1: Load currently offered classes
        IEnumerable<CourseOfferResult> courseOfferResults = 
            await adminCourseScheduleService.GetAllAsync();

        if (!courseOfferResults.Any())
        {
            TempData["ErrorMessage"] = "No available classes at the moment.";
            return RedirectToAction("List", "MyCourse");
        }

        // Step 2: Load currently offered (optional) course materials to buy
        IEnumerable<CourseMaterialResult> courseMaterialResults =
            await adminCourseMaterialService.GetAllAsync();

        // Step 3: Add offered classes and course materials into registration view model
        var viewModel = new CourseRegistrationView()
        {
            CourseSchedules = MapperProfiles
                .MapCourseOfferResultsToCourseScheduleViewModels(courseOfferResults),
            CourseMaterials = mapper.Map<List<CourseMaterialView>>(courseMaterialResults)
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseRegistrationView model)
    {
        if (ModelState.IsValid == false)
        {
            return View();
        }

        Guid courseOfferId = model.SelectedScheduleId;
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        // To set all 'Date' and 'Time' the same
        DateTime currentDateAndTime = DateTime.Now;

        // Create a 'registration'
        Guid registrationId = Guid.NewGuid();

        dynamic registration = new ExpandoObject();
        registration.Id = registrationId;
        registration.StudentId = loginId;
        registration.CourseOfferId = courseOfferId;
        registration.Status = "Unpaid";
        registration.CreatedOn = currentDateAndTime;

        // Create 'order' if there is any material selected to purchase
        Guid orderId = Guid.NewGuid();

        if (model.SelectedMaterialIds != null && model.SelectedMaterialIds.Any())
        {
            dynamic courseMaterialOrder = new ExpandoObject();
            courseMaterialOrder.Id = orderId;
            courseMaterialOrder.RegistrationId = registrationId;
            courseMaterialOrder.OrderStatus = "Unpaid";
            courseMaterialOrder.OrderDate = currentDateAndTime;

            // Create order items
            List<dynamic> orderItems = new();
            foreach (Guid materialId in model.SelectedMaterialIds)
            {
                dynamic orderItem = new ExpandoObject();
                orderItem.CourseMaterialOrderId = orderId;
                orderItem.CourseMaterialId = materialId;
                orderItem.Quantity = 1;

                orderItems.Add(orderItem);
            }

            courseMaterialOrder.CourseMaterialOrderItems = orderItems;
            registration.CourseMaterialOrder = courseMaterialOrder;
        }

        bool isAdded = await registrationService.AddAsync(registration);

        if (isAdded)
        {
            return RedirectToAction("Add", "Payment", new
            {
                registrationId,
                courseOfferId,
                orderId
            });
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong. Failed to register course.";
            return RedirectToAction("List", "Home");
        }
    }
}
