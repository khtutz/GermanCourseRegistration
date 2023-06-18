using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ITimetableRepository
{
    Task<Timetable?> GetByIdAsync(Guid id);

    Task<IEnumerable<Timetable>> GetAllAsync();

    Task<bool> AddAsync(Timetable timetable);

    Task<Timetable?> UpdateAsync(Timetable timetable);

    Task<Timetable?> DeleteAsync(Guid id);

    Task<IEnumerable<Timetable>> DeleteByCouseOfferIdAsync(Guid courseOfferId);
}
