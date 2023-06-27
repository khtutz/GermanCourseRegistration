using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ITimetableRepository
{
    Task<IEnumerable<Timetable>> DeleteByCouseOfferIdAsync(Guid courseOfferId);
}
