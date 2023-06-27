using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IAdminCourseScheduleService
{
    Task<CourseOfferResult> GetByIdAsync(Guid id);

    Task<IEnumerable<CourseOfferResult>> GetAllAsync();

    Task<bool> AddAsync(
        Guid courseId,
        string name,
        string classType,
        decimal cost,
        DateTime startDate,
        DateTime endDate,
        Guid createdBy,
        DateTime createdOn,
        IEnumerable<string> daysOfWeek,
        int StartTimeHour,
        int StartTimeMinute,
        int EndTimeHour,
        int EndTimeMinute);

    Task<CourseOfferResult> UpdateAsync(
        Guid id,
        Guid courseId,
        string name,
        string classType,
        decimal cost,
        DateTime startDate,
        DateTime endDate,
        Guid lastModifiedBy,
        DateTime lastModifiedOn,
        IEnumerable<string> daysOfWeek,
        int StartTimeHour,
        int StartTimeMinute,
        int EndTimeHour,
        int EndTimeMinute);

    Task<CourseOfferResult> DeleteAsync(Guid id);

    IEnumerable<string> GetAvailableClassTypes();

    IEnumerable<string> GetDaysOfWeek();
}
