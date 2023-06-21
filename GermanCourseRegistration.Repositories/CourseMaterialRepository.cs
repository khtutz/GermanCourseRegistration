using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories;

public class CourseMaterialRepository
    : Repository<CourseMaterial, Guid>, ICourseMaterialRepository
{

    public CourseMaterialRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext) { }
}
