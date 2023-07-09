using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMaterialMessages;

public record DeleteCourseMaterialResponse : BaseResponse
{
    public CourseMaterial? CourseMaterial { get; init; }
}