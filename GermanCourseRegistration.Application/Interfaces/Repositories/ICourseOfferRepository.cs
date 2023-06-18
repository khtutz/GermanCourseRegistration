using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ICourseOfferRepository
{
    Task<CourseOffer?> GetByIdAsync(Guid id);

    Task<IEnumerable<CourseOffer>> GetAllAsync();

    Task<bool> AddAsync(CourseOffer courseOffer);

    Task<CourseOffer?> UpdateAsync(CourseOffer courseOffer);

    Task<CourseOffer?> DeleteAsync(Guid id);
}
