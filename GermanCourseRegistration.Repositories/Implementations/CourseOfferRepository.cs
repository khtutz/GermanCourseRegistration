using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class CourseOfferRepository : ICourseOfferRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public CourseOfferRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CourseOffer?> GetByIdAsync(Guid id)
    {
        return await dbContext.CourseOffers
            .Include(c => c.Timetables)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<CourseOffer>> GetAllAsync()
    {
        return await dbContext.CourseOffers
            .Include(c => c.Course)
            .Include(c => c.Timetables)
            .ToListAsync();
    }

    public async Task<CourseOffer> AddAsync(CourseOffer courseOffer)
    {
        await dbContext.AddAsync(courseOffer);
        await dbContext.SaveChangesAsync();

        return courseOffer;
    }

    public async Task<CourseOffer?> UpdateAsync(CourseOffer courseOffer)
    {
        var existingCourseOffer = await dbContext.CourseOffers
            .FirstOrDefaultAsync(c => c.Id == courseOffer.Id);

        if (existingCourseOffer == null) return null;

        existingCourseOffer.Name = courseOffer.Name;
        existingCourseOffer.ClassType = courseOffer.ClassType;
        existingCourseOffer.Cost = courseOffer.Cost;
        existingCourseOffer.StartDate = courseOffer.StartDate;
        existingCourseOffer.EndDate = courseOffer.EndDate;
        existingCourseOffer.LastModifiedBy = courseOffer.LastModifiedBy;
        existingCourseOffer.LastModifiedOn = courseOffer.LastModifiedOn;
        existingCourseOffer.Timetables = courseOffer.Timetables;

        await dbContext.SaveChangesAsync();

        return existingCourseOffer;
    }

    public async Task<CourseOffer?> DeleteAsync(Guid id)
    {
        // Delete the data from both CourseOffers and Timetables tables
        var existingCourseOffer = await dbContext.CourseOffers
            .Include(c => c.Timetables)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existingCourseOffer != null)
        {
            dbContext.CourseOffers.Remove(existingCourseOffer);
            await dbContext.SaveChangesAsync();
        }

        return existingCourseOffer;
    }
}
