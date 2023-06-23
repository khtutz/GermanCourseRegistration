using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories;

public class RegistrationRepository
    : Repository<Registration, Guid>, IRegistrationRepository
{
    public RegistrationRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext) { }
}
