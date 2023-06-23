using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories;

public class PaymentRepository
    : Repository<Payment, Guid>, IPaymentRepository
{
    public PaymentRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext) { }
}
