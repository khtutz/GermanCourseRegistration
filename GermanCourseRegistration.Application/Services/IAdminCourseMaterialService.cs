using GermanCourseRegistration.Application.Messages.CourseMaterialMessages;
using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IAdminCourseMaterialService
{
    Task<GetCourseMaterialByIdResponse> GetByIdAsync(GetCourseMaterialByIdRequest request);

    Task<GetAllCourseMaterialsResponse> GetAllAsync();

    Task<AddCourseMaterialResponse> AddAsync(AddCourseMaterialRequest request);

    Task<UpdateCourseMaterialResponse> UpdateAsync(UpdateCourseMaterialRequest request);

    Task<DeleteCourseMaterialResponse> DeleteAsync(DeleteCourseMaterialRequest request);

    IEnumerable<string> GetCourseMaterialCategories();
}
