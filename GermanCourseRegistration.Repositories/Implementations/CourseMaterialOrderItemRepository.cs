using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class CourseMaterialOrderItemRepository : ICourseMaterialOrderItemRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public CourseMaterialOrderItemRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<CourseMaterialOrderItem>> GetAllByORderIdAsync(Guid orderId)
    {
        try
        {
            return await dbContext.CourseMaterialOrderItems
                .Where(o => o.CourseMaterialOrderId == orderId)
                .Include(o => o.CourseMaterial)
                .ToListAsync();
        }
        catch
        {
            return Enumerable.Empty<CourseMaterialOrderItem>();
        }     
    }
}