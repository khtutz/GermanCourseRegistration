using GermanCourseRegistration.Application.Messages.CourseMessages;

namespace GermanCourseRegistration.Application.Services;

public interface IAdminCourseService
{
    Task<GetCourseByIdResponse> GetByIdAsync(GetCourseByIdRequest request);

    Task<GetAllCoursesResponse> GetAllAsync();

    Task<AddCourseResponse> AddAsync(AddCourseRequest request);

    Task<UpdateCourseResponse> UpdateAsync(UpdateCourseRequest request);

    Task<DeleteCourseResponse> DeleteAsync(DeleteCourseRequest request);
}
