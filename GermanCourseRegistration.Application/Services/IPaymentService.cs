using GermanCourseRegistration.Application.Messages.PaymentMessages;

namespace GermanCourseRegistration.Application.Services;

public interface IPaymentService
{
    Task<AddPaymentResponse> AddAsync(AddPaymentRequest request);
}
