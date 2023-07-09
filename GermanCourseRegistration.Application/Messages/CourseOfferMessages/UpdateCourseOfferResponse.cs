using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseOfferMessages;

public record UpdateCourseOfferResponse : BaseResponse
{
    public CourseOffer? CourseOffer { get; init; }
}
