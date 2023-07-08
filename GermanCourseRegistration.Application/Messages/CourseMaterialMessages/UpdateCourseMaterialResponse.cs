using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMaterialMessages;

public record UpdateCourseMaterialResponse() : BaseResponse
{
    public CourseMaterial? CourseMaterial { get; init; }
}