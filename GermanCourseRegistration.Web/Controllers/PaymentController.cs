using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

public class PaymentController : Controller
{
    private readonly IAdminCourseScheduleService adminCourseScheduleService;
    private readonly ICartService cartService;
    private readonly IPaymentService paymentService;

    public PaymentController(
        IAdminCourseScheduleService adminCourseScheduleService,
        ICartService cartService,
        IPaymentService paymentService)
    {
        this.adminCourseScheduleService = adminCourseScheduleService;
        this.cartService = cartService;
        this.paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IActionResult> Add(Guid registrationId, Guid courseOfferId, Guid orderId)
    {
        // Get the cost of selected course
        var courseOfferResult = await adminCourseScheduleService.GetByIdAsync(courseOfferId);
        decimal courseCost = courseOfferResult?.CouseOffer?.Cost ?? 0;

        // Get purchased items
        var order = await cartService.GetItemsByOrderIdAsync(orderId);

        // Map to view model
        var viewModel = new PaymentView()
        {
            RegistrationId = registrationId,
            Amount = courseCost + cartService.CalculateTotalAmount(order)
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PaymentView viewModel)
    {
        // Mock Payment
        // Information will be sent to third party payment gateway
        // Application will continue based on the result from the payment

        // It is assumed that payment is successful here
        bool isAdded = await paymentService.AddAsync(
            "Credit/Debit",
            viewModel.Amount,
            "Success");

        if (isAdded)
        {
            TempData["SuccessMessage"] = "Course has been registered successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to register course.";
        }

        return RedirectToAction("Index", "Home");
    }
}
