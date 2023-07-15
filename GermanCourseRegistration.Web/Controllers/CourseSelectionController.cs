using AutoMapper;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.HelperServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GermanCourseRegistration.Application.Messages.RegistrationMessages;

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
        var offeredCoursesResponse = await adminCourseScheduleService.GetAllAsync();

        if (offeredCoursesResponse == null || !offeredCoursesResponse.CourseOffers.Any())
        {
            TempData["ErrorMessage"] = "No available classes at the moment.";
            return RedirectToAction("List", "MyCourse");
        }

        // Step 2: Load currently offered (optional) course materials to buy
        var courseMaterialsResponse = await adminCourseMaterialService.GetAllAsync();

        // Step 3: Add offered classes and course materials into registration view model
        var viewModel = new CourseRegistrationView()
        {
            CourseSchedules = CourseScheduleMapping.MapToViewModels(offeredCoursesResponse),
            CourseMaterials = CourseMaterialMapping.MapToViewModels(courseMaterialsResponse)
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseRegistrationView model)
    {
        Guid courseOfferId = model.SelectedScheduleId;
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        // To set all 'Date' and 'Time' the same
        DateTime currentDateAndTime = DateTime.Now;

        // Step 1: Create a registration request
        Guid registrationId = Guid.NewGuid();

        var registrationRequest = RegistrationMapping.MapToRegistrationAddRequest(
            registrationId, loginId, courseOfferId, "Unpaid", currentDateAndTime);

        // Step 2: Create an order request
        Guid orderId = Guid.NewGuid();
        var orderRequest = new AddOrderRequest();
        var orderItems = new List<AddOrderItemRequest>();

        if (model.SelectedMaterialIds != null && model.SelectedMaterialIds.Any())
        {
            orderRequest = RegistrationMapping.MapToOrderRequest(
                orderId, registrationId, "Unpaid", currentDateAndTime);

            // Step 3: Create order items
            foreach (Guid materialId in model.SelectedMaterialIds)
            {
                var orderItemRequest = RegistrationMapping.MapToOrderItemRequest(
                    orderId, materialId, 1);
                orderItems.Add(orderItemRequest);
            }
        }
        var orderItemsRequest = new AddOrderItemsRequest(orderItems);

        var response = await registrationService.AddAsync(
            registrationRequest,
            orderRequest,
            orderItemsRequest);

        if (response.IsTransactionSuccess)
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
