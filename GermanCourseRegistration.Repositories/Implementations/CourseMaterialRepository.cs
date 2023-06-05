using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class CourseMaterialRepository : ICourseMaterialRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public CourseMaterialRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CourseMaterial?> GetByIdAsync(Guid id)
    {
        try
        {
            return await dbContext.CourseMaterials
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<CourseMaterial>> GetAllAsync()
    {
        try
        {
            return await dbContext.CourseMaterials.ToListAsync();
        }
        catch
        {
            return Enumerable.Empty<CourseMaterial>();
        }    
    }

    public async Task<bool> AddAsync(CourseMaterial courseMaterial)
    {
        try
        {
            await dbContext.AddAsync(courseMaterial);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CourseMaterial?> UpdateAsync(CourseMaterial courseMaterial)
    {
        try
        {
            var existingCourseMaterial = await dbContext.CourseMaterials
            .FirstOrDefaultAsync(x => x.Id == courseMaterial.Id);

            if (existingCourseMaterial == null) return null;

            existingCourseMaterial.Name = courseMaterial.Name;
            existingCourseMaterial.Description = courseMaterial.Description;
            existingCourseMaterial.Category = courseMaterial.Category;
            existingCourseMaterial.Price = courseMaterial.Price;
            existingCourseMaterial.LastModifiedBy = courseMaterial.LastModifiedBy;
            existingCourseMaterial.LastModifiedOn = courseMaterial.LastModifiedOn;

            await dbContext.SaveChangesAsync();

            return existingCourseMaterial;
        }
        catch
        {
            return null;
        }  
    }

    public async Task<CourseMaterial?> DeleteAsync(Guid id)
    {
        try
        {
            var existingCourseMaterial = await dbContext.CourseMaterials.FindAsync(id);

            if (existingCourseMaterial != null)
            {
                dbContext.CourseMaterials.Remove(existingCourseMaterial);
                await dbContext.SaveChangesAsync();
            }

            return existingCourseMaterial;
        }
        catch
        {
            return null;
        }
    }
}
