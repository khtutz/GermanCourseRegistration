using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories;

public class CourseOfferRepository : Repository<CourseOffer, Guid>, ICourseOfferRepository
{
    public CourseOfferRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext) { }
}
