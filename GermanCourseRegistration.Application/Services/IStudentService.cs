using GermanCourseRegistration.Application.Messages.StudentMessages;
using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IStudentService
{
    Task<GetStudentByIdResponse> GetByIdAsync(GetStudentByIdRequest request);

    Task<AddStudentResponse> AddAsync(AddStudentRequest request);

    Task<UpdateStudentResponse> UpdateAsync(UpdateStudentRequest request);

    IEnumerable<string> GetSalutations();
}
