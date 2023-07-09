using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.StudentMessages;

public record GetStudentByIdResponse : BaseResponse
{
    public Student? Student { get; init; }
}
