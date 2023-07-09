using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.StudentMessages;

public record UpdateStudentResponse : BaseResponse
{
    public Student? Student { get; init; }
}