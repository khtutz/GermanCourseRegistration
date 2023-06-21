using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

public class CourseMaterialOrderItemRepository : ICourseMaterialOrderItemRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public CourseMaterialOrderItemRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<CourseMaterialOrderItem>> GetAllByOrderIdAsync(Guid orderId)
    {
        return await dbContext.CourseMaterialOrderItems
                .Where(o => o.CourseMaterialOrderId == orderId)
                .Include(o => o.CourseMaterial)
                .ToListAsync();
    }
}
