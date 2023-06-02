using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public RegistrationRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Registration?> GetByStudentIdAsync(Guid id)
    {
        return await dbContext.Registrations
            .Include(r => r.CourseOffer)
            .FirstOrDefaultAsync(r => r.StudentId == id);
    }

    public async Task<Registration> AddAsync(Registration registration)
    {
        await dbContext.AddAsync(registration);
        await dbContext.SaveChangesAsync();

        return registration;
    }
}
