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

    public async Task<bool> AddAsync(Payment payment)
    {
        try
        {
            await dbContext.AddAsync(payment);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
    }
}
