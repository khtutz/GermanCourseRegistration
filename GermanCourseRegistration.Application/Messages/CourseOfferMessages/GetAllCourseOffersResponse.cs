using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.CourseOfferMessages;

public record GetAllCourseOffersResponse : BaseResponse
{
    public IEnumerable<CourseOffer> CourseOffers { get; init; }
}