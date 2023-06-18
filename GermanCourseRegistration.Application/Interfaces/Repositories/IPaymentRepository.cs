using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<bool> AddAsync(Payment payment);
}
