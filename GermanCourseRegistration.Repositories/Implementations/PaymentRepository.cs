using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;

namespace GermanCourseRegistration.Repositories.Implementations;

public class PaymentRepository : IPaymentRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public PaymentRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await dbContext.AddAsync(payment);
        await dbContext.SaveChangesAsync();

        return payment;
    }
}
