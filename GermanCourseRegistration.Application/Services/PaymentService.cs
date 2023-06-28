using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<bool> AddAsync(string paymentMethod, decimal amount, string paymentStatus)
    {
        var payment = new Payment()
        {
            PaymentMethod = paymentMethod,
            Amount = amount,
            PaymentStatus = paymentStatus
        };

        bool isAdded = await paymentRepository.AddAsync(payment);

        return isAdded;
    }
}
