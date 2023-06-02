using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
}
