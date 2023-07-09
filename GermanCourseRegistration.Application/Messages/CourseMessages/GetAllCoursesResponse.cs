using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMessages;

public record GetAllCoursesResponse : BaseResponse
{
    public IEnumerable<Course> Courses { get; init; }
}