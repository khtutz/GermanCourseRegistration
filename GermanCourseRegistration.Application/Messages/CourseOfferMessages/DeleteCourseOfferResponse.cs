using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseOfferMessages;

public record DeleteCourseOfferResponse() : BaseResponse
{
    public CourseOffer? CourseOffer { get; init; }
}
