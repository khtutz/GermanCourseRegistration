using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

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

    public async Task<bool> AddAsync(CourseOffer entity)
    {
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<CourseOffer?> UpdateAsync(CourseOffer entity, Guid id)
    {
        var existingCourseOffer = await dbContext.CourseOffers
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existingCourseOffer == null)
        {
            return null;
        }

        existingCourseOffer.Name = entity.Name;
        existingCourseOffer.ClassType = entity.ClassType;
        existingCourseOffer.Cost = entity.Cost;
        existingCourseOffer.StartDate = entity.StartDate;
        existingCourseOffer.EndDate = entity.EndDate;
        existingCourseOffer.LastModifiedBy = entity.LastModifiedBy;
        existingCourseOffer.LastModifiedOn = entity.LastModifiedOn;
        existingCourseOffer.Timetables = entity.Timetables;

        await dbContext.SaveChangesAsync();

        return existingCourseOffer;
    }

    public async Task<CourseOffer?> DeleteAsync(Guid id)
    {
        var existingCourseOffer = await dbContext.CourseOffers
            .Include(c => c.Timetables)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existingCourseOffer == null)
        {
            return null;
        }

        dbContext.CourseOffers.Remove(existingCourseOffer);
        await dbContext.SaveChangesAsync();

        return existingCourseOffer;
    } 
}
