using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

public class TimetableRepository : ITimetableRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public TimetableRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Timetable>> DeleteByCouseOfferIdAsync(Guid courseOfferId)
    {
        var existingTimetables = await dbContext.Timetables
            .Where(t => t.CourseOfferId == courseOfferId)
            .ToListAsync();

        if (existingTimetables != null && existingTimetables.Any())
        {
            dbContext.Timetables.RemoveRange(existingTimetables);
            await dbContext.SaveChangesAsync();

            return existingTimetables;
        }

        return Enumerable.Empty<Timetable>();
    }
}
