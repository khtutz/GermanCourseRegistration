using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMaterialMessages;

public record GetCourseMaterialByIdResponse : BaseResponse
{
    public CourseMaterial? CourseMaterial { get; init; }
}