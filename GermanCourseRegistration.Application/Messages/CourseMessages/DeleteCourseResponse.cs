using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMessages;

public record DeleteCourseResponse : BaseResponse
{
    public Course? Course { get; init; }
}