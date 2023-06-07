using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Implementations;
using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

public class PaymentController : Controller
{
    private readonly ICourseOfferRepository courseOfferRepository;
    private readonly ICourseMaterialOrderItemRepository orderItemRepository;
    private readonly IPaymentRepository paymentRepository;

    public PaymentController(
        ICourseOfferRepository courseOfferRepository,
        ICourseMaterialOrderItemRepository orderItemRepository,
        IPaymentRepository paymentRepository)
    {
        this.courseOfferRepository = courseOfferRepository;
        this.orderItemRepository = orderItemRepository;
        this.paymentRepository = paymentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Add(Guid registrationId, Guid courseOfferId, Guid orderId)
    {
        CourseOffer? courseOffer = null;

        // Get selected course
        try
        {
            courseOffer = await courseOfferRepository.GetByIdAsync(courseOfferId);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        decimal courseCost = courseOffer != null ? courseOffer.Cost : 0;

        // Get purchased item(s)
        var orderItems = Enumerable.Empty<CourseMaterialOrderItem>();

        try
        {
            orderItems = await orderItemRepository.GetAllByOrderIdAsync(orderId);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (!orderItems.Any())
        {
            // Show error message
            return View(new PaymentView());
        }

        var model = new PaymentView()
        {
            RegistrationId = registrationId,
            Amount = courseCost + OrderCalculationService.CalculateTotalAmount(orderItems)
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PaymentView model)
    {
        // Mock Payment
        // Information will be sent to thired party payment gateway
        // Application will continue based on the result from the payment

        // It is assumed that payment is successful here
        var payment = new Payment()
        {
            PaymentMethod = "Credit/Debit",
            Amount = model.Amount,
            PaymentStatus = Payment.PaymentSuccess
        };

        bool isAdded = false;

        try
        {
            isAdded = await paymentRepository.AddAsync(payment);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        if (isAdded)
        {
            // Show success notification
            return RedirectToAction("Index", "Home");
        }

        // Show error notification
        return RedirectToAction("Index", "Home");
    }
}
