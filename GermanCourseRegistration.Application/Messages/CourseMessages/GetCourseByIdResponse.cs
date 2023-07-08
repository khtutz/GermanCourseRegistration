using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMessages;

public record GetCourseByIdResponse : BaseResponse
{
    public Course? Course { get; init; }
}
