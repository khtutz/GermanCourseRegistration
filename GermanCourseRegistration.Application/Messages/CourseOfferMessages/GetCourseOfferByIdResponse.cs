using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseOfferMessages;

public record GetCourseOfferByIdResponse : BaseResponse
{
    public CourseOffer? CourseOffer { get; init; }
}
