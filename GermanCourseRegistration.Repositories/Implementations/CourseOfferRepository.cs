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
        try
        {
            return await dbContext.CourseOffers
                .Include(c => c.Timetables)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<CourseOffer>> GetAllAsync()
    {
        try
        {
            return await dbContext.CourseOffers
            .Include(c => c.Course)
            .Include(c => c.Timetables)
            .ToListAsync();
        }
        catch
        {
            return Enumerable.Empty<CourseOffer>();
        }  
    }

    public async Task<bool> AddAsync(CourseOffer courseOffer)
    {
        try
        {
            await dbContext.AddAsync(courseOffer);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CourseOffer?> UpdateAsync(CourseOffer courseOffer)
    {
        try
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
        catch
        {
            return null;
        } 
    }

    public async Task<CourseOffer?> DeleteAsync(Guid id)
    {
        try
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
        catch
        {
            return null;
        }
    }
}
