using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<bool> AddAsync(Payment payment);
}
