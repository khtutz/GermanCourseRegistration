using GermanCourseRegistration.Application.Messages.CourseOfferMessages;

namespace GermanCourseRegistration.Application.Services;

public interface IAdminCourseScheduleService
{
    Task<GetCourseOfferByIdResponse> GetByIdAsync(GetCourseOfferByIdRequest request);

    Task<GetAllCourseOffersResponse> GetAllAsync();

    Task<AddCourseOfferResponse> AddAsync(AddCourseOfferRequest request);

    Task<UpdateCourseOfferResponse> UpdateAsync(UpdateCourseOfferRequest request);

    Task<DeleteCourseOfferResponse> DeleteAsync(DeleteCourseOfferRequest request);

    IEnumerable<string> GetAvailableClassTypes();

    IEnumerable<string> GetDaysOfWeek();
}
