using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface ITimetableRepository
{
    Task<Timetable?> GetByIdAsync(Guid id);

    Task<IEnumerable<Timetable>> GetAllAsync();

    Task<Timetable> AddAsync(Timetable timetable);

    Task<Timetable?> UpdateAsync(Timetable timetable);

    Task<Timetable?> DeleteAsync(Guid id);

    Task<IEnumerable<Timetable>?> DeleteByCouseOfferIdAsync(Guid courseOfferId);
}
