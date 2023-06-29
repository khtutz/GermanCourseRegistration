using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

public class RegistrationRepository
    : Repository<Registration, Guid>, IRegistrationRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public RegistrationRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext) 
    {
        this.dbContext = dbContext;
    }

    public async Task<Registration?> GetByStudentIdAsync(Guid id)
    {
        return await dbContext.Registrations
            .Include(r => r.CourseOffer)
            .FirstOrDefaultAsync(r => r.StudentId == id);
    }
}
