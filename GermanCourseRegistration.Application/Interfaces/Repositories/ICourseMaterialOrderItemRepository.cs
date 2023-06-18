using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ICourseMaterialOrderItemRepository
{
    Task<IEnumerable<CourseMaterialOrderItem>> GetAllByOrderIdAsync(Guid orderId);
}
