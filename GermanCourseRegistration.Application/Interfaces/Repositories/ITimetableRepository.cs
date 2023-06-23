using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ITimetableRepository : IRepository<Timetable, Guid>
{
    Task<IEnumerable<Timetable>> DeleteByCouseOfferIdAsync(Guid courseOfferId);
}
