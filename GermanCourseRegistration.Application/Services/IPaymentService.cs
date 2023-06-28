namespace GermanCourseRegistration.Application.Services;

public interface IPaymentService
{
    Task<bool> AddAsync(string paymentMethod, decimal amount, string paymentStatus);
}
