using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface ICartService
{
    Task<MaterialOrdersResult> GetItemsByOrderIdAsync(Guid id);

    decimal CalculateTotalAmount(MaterialOrdersResult order);
}
