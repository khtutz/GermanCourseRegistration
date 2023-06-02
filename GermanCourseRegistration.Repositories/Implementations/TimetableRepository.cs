using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.DataContext;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class TimetableRepository : ITimetableRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public TimetableRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Timetable?> GetByIdAsync(Guid id)
    {
        return await dbContext.Timetables
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Timetable>> GetAllAsync()
    {
        return await dbContext.Timetables.ToListAsync();
    }

    public async Task<Timetable> AddAsync(Timetable timetable)
    {
        await dbContext.AddAsync(timetable);
        await dbContext.SaveChangesAsync();

        return timetable;
    }

    public async Task<Timetable?> UpdateAsync(Timetable timetable)
    {
        var existingTimetable = await dbContext.Timetables
             .FirstOrDefaultAsync(c => c.Id == timetable.Id);

        if (existingTimetable == null) return null;

        existingTimetable.DayName = timetable.DayName;
        existingTimetable.StartTimeHour = timetable.StartTimeHour;
        existingTimetable.StartTimeMinute = timetable.StartTimeMinute;
        existingTimetable.EndTimeHour = timetable.EndTimeHour;
        existingTimetable.EndTimeMinute = timetable.EndTimeMinute;

        return existingTimetable;
    }

    public async Task<Timetable?> DeleteAsync(Guid id)
    {
        var existingTimetable = await dbContext.Timetables.FindAsync(id);

        if (existingTimetable != null)
        {
            dbContext.Timetables.Remove(existingTimetable);
            await dbContext.SaveChangesAsync();
        }

        return existingTimetable;
    }

    public async Task<IEnumerable<Timetable>?> DeleteByCouseOfferIdAsync(Guid courseOfferId)
    {
        var existingTimetables = await dbContext.Timetables
            .Where(t => t.CourseOfferId == courseOfferId)
            .ToListAsync();

        if (existingTimetables != null && existingTimetables.Any())
        {
            dbContext.Timetables.RemoveRange(existingTimetables);
            await dbContext.SaveChangesAsync();
        }

        return existingTimetables;
    }

}
