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
        try
        {
            return await dbContext.Timetables
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Timetable>> GetAllAsync()
    {
        try
        {
            return await dbContext.Timetables.ToListAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> AddAsync(Timetable timetable)
    {
        try
        {
            await dbContext.AddAsync(timetable);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Timetable?> UpdateAsync(Timetable timetable)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public async Task<Timetable?> DeleteAsync(Guid id)
    {
        try
        {
            var existingTimetable = await dbContext.Timetables.FindAsync(id);

            if (existingTimetable != null)
            {
                dbContext.Timetables.Remove(existingTimetable);
                await dbContext.SaveChangesAsync();
            }

            return existingTimetable;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Timetable>> DeleteByCouseOfferIdAsync(Guid courseOfferId)
    {
        try
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
        catch
        {
            throw;
        }
    }

}
