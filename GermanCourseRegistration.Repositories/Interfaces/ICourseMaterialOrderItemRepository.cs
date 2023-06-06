using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface ICourseMaterialOrderItemRepository
{
    Task<IEnumerable<CourseMaterialOrderItem>> GetAllByOrderIdAsync(Guid orderId);
}
