using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMessages;

public record UpdateCourseResponse : BaseResponse
{
    public Course? Course { get; init; }
}
