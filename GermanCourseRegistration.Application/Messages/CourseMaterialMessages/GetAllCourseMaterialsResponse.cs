using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseMaterialMessages;

public record GetAllCourseMaterialsResponse : BaseResponse
{
    public IEnumerable<CourseMaterial> CourseMaterials { get; init; }
}